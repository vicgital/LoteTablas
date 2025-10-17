using LoteTablas.Grpc.Document.Application.Contracts;
using LoteTablas.Grpc.Document.Application.Features.PdfGenerator;
using LoteTablas.Grpc.Document.Infrastructure.Components;
using LoteTablas.Infrastructure.Configuration.Constants;
using LoteTablas.Infrastructure.Configuration.Extensions;
using LoteTablas.Infrastructure.Configuration.Helpers;
using LoteTablas.Infrastructure.Logging.Extensions;

namespace LoteTablas.Grpc.Document.Service
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

            // Add MediatR
            services.AddMediatR(cfg =>
            {
                var assebly = typeof(PdfGeneratorHandler).Assembly;
                cfg.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.MEDIATR_LICENSE_KEY);
                cfg.RegisterServicesFromAssembly(assebly);
            });

            // Add Components
            services.AddScoped<IPdfGeneratorComponent, PdfGeneratorComponent>();

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
                endpoints.MapGrpcService<DocumentService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

        }
    }
}
