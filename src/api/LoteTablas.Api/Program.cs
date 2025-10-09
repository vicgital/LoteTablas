
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace LoteTablas.Api;

/// <summary>
/// Program
/// </summary>
public static class Program
{
    /// <summary>
    /// Main Method
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        BuildWebHost(args).Run();
    }

    /// <summary>
    /// BuildWebHost
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IWebHost BuildWebHost(string[] args) =>
       WebHost.CreateDefaultBuilder(args)
           .UseKestrel(opt => opt.AddServerHeader = false)
           .ConfigureKestrel(options =>
           {
               options.AddServerHeader = false;
               options.ListenAnyIP(5000, o => o.Protocols =
                   HttpProtocols.Http1AndHttp2);
           })
          .UseStartup<Startup>()
          .Build();
}