using BlazorAnimate;
using LoteTablas.Blazor.UI;
using LoteTablas.Blazor.UI.Data.Repositories.Definition;
using LoteTablas.Blazor.UI.Data.Repositories.Implementation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();


AddApiRepositories(builder, configuration);


builder.Services.AddBlazorBootstrap();

ConfigureAnimations(builder);

await builder.Build().RunAsync();



static void AddApiRepositories(WebAssemblyHostBuilder builder, IConfigurationRoot configuration)
{

    var isTest = configuration.GetValue<bool>("IsTestMode", false);

    var apiBaseUrl = isTest ? builder.HostEnvironment.BaseAddress : configuration.GetValue<string>("ApiBaseUrl");

    builder.Services.AddHttpClient("api", options =>
    {
        options.BaseAddress = new Uri(apiBaseUrl ?? throw new Exception("ApiBaseUrl in settings file"));
    });


    builder.Services.AddScoped<ILotteryRepository, LotteryRepository>();
    builder.Services.AddScoped<IBoardRepository, BoardRepository>();
}

static void ConfigureAnimations(WebAssemblyHostBuilder builder)
{
    builder.Services.Configure<AnimateOptions>("fadeIn", options =>
    {
        options.Animation = Animations.FadeIn;
        options.Duration = TimeSpan.FromSeconds(0.5);
        options.Once = true;
    });
}
