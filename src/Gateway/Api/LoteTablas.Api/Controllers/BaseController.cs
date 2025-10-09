using Microsoft.AspNetCore.Mvc;

namespace LoteTablas.Api.Controllers
{
    public class BaseController<T>(ILogger<T> logger, IConfiguration config) : Controller where T : BaseController<T>
    {
        protected readonly ILogger<T> _logger = logger;
        protected readonly IConfiguration _config = config;
    }
}
