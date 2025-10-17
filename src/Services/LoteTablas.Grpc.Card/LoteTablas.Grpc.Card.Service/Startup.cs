using LoteTablas.Grpc.Card.Application.Contracts.Persistence;
using LoteTablas.Grpc.Card.Application.Features.Card.Handlers;
using LoteTablas.Grpc.Card.Infrastructure.Repositories;
using LoteTablas.Grpc.Card.Service.AutoMapper;
using LoteTablas.Infrastructure.Cache.Extensions;
using LoteTablas.Infrastructure.Configuration.Constants;
using LoteTablas.Infrastructure.Configuration.Extensions;
using LoteTablas.Infrastructure.Configuration.Helpers;
using LoteTablas.Infrastructure.Database.Extensions;
using LoteTablas.Infrastructure.Logging.Extensions;

namespace LoteTablas.Grpc.Card.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var config = ConfigurationHelper.GetConfiguration();

            // Add services to the container.
            services.AddGrpc();

            services.AddConfigurationManager(config);

            // Add Logging
            services.AddSerilogLogging(config);

            // Add Cache
            services.AddInMemoryCache();

            // Add Database
            services.AddLoteTablasMongoDatabaseConnection(config);

            // Add MediatR
            services.AddMediatR(cfg =>
            {
                var assebly = typeof(GetCardHandler).Assembly;
                cfg.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.MEDIATR_LICENSE_KEY);
                cfg.RegisterServicesFromAssembly(assebly);
            });

            // Add AutoMapper
            services.AddAutoMapper(config =>
            {
                config.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AUTOMAPPER_LICENSE_KEY);
            }, typeof(MappingProfile));

            // Add Repositories
            services.AddScoped<ICardRepository, CardRepository>();

            services.AddApplicationInsightsTelemetry();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CardService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

        }
    }
}
