using Grpc.Net.Client;
using LoteTablas.Api.Application.Contracts.Clients.Grpc;
using LoteTablas.Api.Application.Features.Board.Requests;
using LoteTablas.Api.AutoMapper;
using LoteTablas.Api.Infrastructure.Clients.Grpc;
using LoteTablas.Infrastructure.Configuration.Constants;
using LoteTablas.Infrastructure.Configuration.Extensions;
using LoteTablas.Infrastructure.Configuration.Helpers;
using LoteTablas.Infrastructure.Logging.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using static LoteTablas.Grpc.Definitions.Board;
using static LoteTablas.Grpc.Definitions.Lottery;

namespace LoteTablas.Api;

public class Startup
{

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public void ConfigureServices(IServiceCollection services)
    {

        var config = ConfigurationHelper.GetConfiguration();

        services.AddControllers();

        services.AddConfigurationManager(config);

        // Add Logging
        services.AddSerilogLogging(config);

        // Add MediatR
        services.AddMediatR(cfg =>
        {
            var assebly = typeof(GetBoardDocumentsRequest).Assembly;
            cfg.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.MEDIATR_LICENSE_KEY);
            cfg.RegisterServicesFromAssembly(assebly);
        });


        // Add AutoMapper
        services.AddAutoMapper(config =>
        {
            config.LicenseKey = Environment.GetEnvironmentVariable(EnvironmentVariableNames.AUTOMAPPER_LICENSE_KEY);
        }, typeof(MappingProfile));


        // Add Grpc Dependencies
        var boardServiceGrpcUrl = Environment.GetEnvironmentVariable(EnvironmentVariableNames.BOARD_SERVICE_GRPC_URL) ?? throw new Exception($"{EnvironmentVariableNames.BOARD_SERVICE_GRPC_URL} NOT FOUND AS ENVIRONMENT VARIABLE");
        services.AddSingleton(new BoardClient(
            GrpcChannel.ForAddress(
                    boardServiceGrpcUrl
                )));


        var lotteryServiceGrpcUrl = Environment.GetEnvironmentVariable(EnvironmentVariableNames.LOTTERY_SERVICE_GRPC_URL) ?? throw new Exception($"{EnvironmentVariableNames.LOTTERY_SERVICE_GRPC_URL} NOT FOUND AS ENVIRONMENT VARIABLE");
        services.AddSingleton(new LotteryClient(
            GrpcChannel.ForAddress(
                    lotteryServiceGrpcUrl
                )));


        services.AddSingleton<IBoardGrpcClient, BoardGrpcClient>();
        services.AddSingleton<ILotteryGrpcClient, LotteryGrpcClient>();


        services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
            options.MemoryBufferThreshold = int.MaxValue;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

        });

        services.AddApplicationInsightsTelemetry();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });



    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="httpContextAccessor"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        app.UseForwardedHeaders();

        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseCors("AllowAll");

        app.UseEndpoints(endpoints =>
        {
            // Mapping of endpoints goes here:
            endpoints.MapControllers();
        });

        Log.Information("LoteTablas.Api ready..");

    }

}
