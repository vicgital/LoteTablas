using Grpc.Net.Client;
using LoteTablas.Api.AutoMapper;
using LoteTablas.Api.Business.Components.Definition;
using LoteTablas.Api.Business.Components.Implementation;
using LoteTablas.Api.Data.Repositories.Definition;
using LoteTablas.Api.Data.Repositories.Implementation;
using LoteTablas.Framework.Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using static LoteTablas.Core.Service.Definition.Core;

namespace LoteTablas.Api;

public class Startup
{

    /// <summary>
    /// Configuration
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="env">IWebHostEnvironment</param>
    public Startup()
    {
        Configuration = StartupHelper.GetConfiguration();
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();


        // Add Components and Repositories
        services
        .AddSingleton(Configuration)
        .AddSerilogLogging(Configuration)
        .AddAutoMapper(typeof(MappingProfile))

        .AddSingleton<IBoardComponent, BoardComponent>()
        .AddSingleton<ICardComponent, CardComponent>()
        .AddSingleton<ILotteryComponent, LotteryComponent>()

        .AddSingleton<IBoardRepository, BoardRepository>()
        .AddSingleton<ICardRepository, CardRepository>()
        .AddSingleton<ILotteryRepository, LotteryRepository>()

        .AddSingleton<CoreClient>(new CoreClient(GrpcChannel.ForAddress(Environment.GetEnvironmentVariable("GRPC_CORE_SERVICE_URL") ?? throw new Exception("GRPC_CORE_SERVICE_URL NOT FOUND AS ENVIRONMENT VARIABLE"))))

        //.AddGrpcUpstreams(configuration, new()
        //{
        //    ["core"] = typeof(CoreClient),
        //})

        .Configure<FormOptions>(options =>
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
