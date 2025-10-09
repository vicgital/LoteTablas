using LoteTablas.Core.Business;
using LoteTablas.Core.Business.Interfaces;
using LoteTablas.Core.Data;
using LoteTablas.Core.Data.Interfaces;
using LoteTablas.Core.Service.AutoMapper;
using LoteTablas.Core.Service.Services;
using LoteTablas.Framework.Common;
using Serilog;

namespace LoteTablas.Core.Service;

public class Startup
{
    public static IConfiguration? Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
        GetConfiguration();

        // // Add services to the container.
        services.AddGrpc();

        // // Add Components and Repositories

        services
             .AddConfiguration(Configuration ?? throw new InvalidOperationException("Can't Get Configuration"))
             .AddSerilogLogging(Configuration)
             .AddInMemoryCache()
             .AddBlobService()
             .AddAutoMapper(typeof(MappingProfile))
             .AddSingleton<ILotteryComponent, LotteryComponent>()
             .AddSingleton<ICardComponent, CardComponent>()
             .AddSingleton<IBoardComponent, BoardComponent>()
             .AddSingleton<IPdfGeneratorComponent, PdfGeneratorComponent>();

        bool useDb = bool.Parse(Environment.GetEnvironmentVariable("USE_DB") ?? "false");

        if (!useDb)
        {
            services
            .AddSingleton<ILotteryRepository, LoteTablas.Core.Data.Mock.LotteryRepository>()
            .AddSingleton<ICardRepository, LoteTablas.Core.Data.Mock.CardRepository>()
            .AddSingleton<IBoardRepository, LoteTablas.Core.Data.Mock.BoardRepository>();
        }
        else
        {
            services
                .AddDatabaseConnection()
                .AddSingleton<ILotteryRepository, LotteryRepository>()
                .AddSingleton<ICardRepository, CardRepository>()
                .AddSingleton<IBoardRepository, BoardRepository>();
        }


        services.AddApplicationInsightsTelemetry();

        Log.Information("Service Ready..");

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<CoreService>();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            });
        });
    }

    private static void GetConfiguration()
    {
        Configuration = StartupHelper.GetConfiguration();
    }

}
