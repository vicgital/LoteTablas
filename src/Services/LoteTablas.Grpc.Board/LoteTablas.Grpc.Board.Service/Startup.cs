using Grpc.Net.Client;
using LoteTablas.Grpc.Board.Application.Contracts.Clients.Grpc;
using LoteTablas.Grpc.Board.Application.Contracts.Components;
using LoteTablas.Grpc.Board.Application.Contracts.Persistence;
using LoteTablas.Grpc.Board.Application.Features.BoardDocument.Handlers;
using LoteTablas.Grpc.Board.Infrastructure.Clients.Grpc;
using LoteTablas.Grpc.Board.Infrastructure.Components;
using LoteTablas.Grpc.Board.Infrastructure.Repositories;
using LoteTablas.Grpc.Board.Service.AutoMapper;
using LoteTablas.Infrastructure.Configuration.Constants;
using LoteTablas.Infrastructure.Configuration.Extensions;
using LoteTablas.Infrastructure.Configuration.Helpers;
using LoteTablas.Infrastructure.Logging.Extensions;


namespace LoteTablas.Grpc.Board.Service
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
                var assebly = typeof(GetBoardDocumentHandler).Assembly;
                cfg.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.MEDIATR_LICENSE_KEY);
                cfg.RegisterServicesFromAssembly(assebly);
            });

            // Add AutoMapper
            services.AddAutoMapper(config =>
            {
                config.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AUTOMAPPER_LICENSE_KEY);
            }, typeof(MappingProfile));



            // Add Grpc Dependencies
            var cardServiceGrpcUrl = Environment.GetEnvironmentVariable(EnvironmentVariableNames.CARD_SERVICE_GRPC_URL) ?? throw new Exception($"{EnvironmentVariableNames.CARD_SERVICE_GRPC_URL} NOT FOUND AS ENVIRONMENT VARIABLE");
            services.AddSingleton(new Definitions.Card.CardClient(
                GrpcChannel.ForAddress(
                        cardServiceGrpcUrl
                    )));


            var documentServiceGrpcUrl = Environment.GetEnvironmentVariable(EnvironmentVariableNames.DOCUMENT_SERVICE_GRPC_URL) ?? throw new Exception($"{EnvironmentVariableNames.DOCUMENT_SERVICE_GRPC_URL} NOT FOUND AS ENVIRONMENT VARIABLE");
            services.AddSingleton(new Definitions.Document.DocumentClient(
                GrpcChannel.ForAddress(
                    documentServiceGrpcUrl
                    )));

            // Add Grpc Clients
            services.AddScoped<ICardGrpcClient, CardGrpcClient>();
            services.AddScoped<IDocumentGrpcClient, DocumentGrpcClient>();

            // Add Components
            services.AddScoped<IBoardBuilderComponent, BoardBuilderComponent>();

            // Add Repositories
            services.AddScoped<IBoardRepository, BoardRepository>();

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
                endpoints.MapGrpcService<BoardService>();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });

        }
    }
}
