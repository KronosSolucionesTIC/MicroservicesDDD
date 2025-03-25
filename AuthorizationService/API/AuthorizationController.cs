using AuthorizationService;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.API
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            _logger.LogInformation("Esto es un log de prueba desde la aplicación.");

            try
            {
                var rng = new Random();

                if (rng.Next(0, 5) < 2)
                {
                    throw new Exception("Opps");
                }

                return Ok(Enumerable.Range(1, 5).Select(index => new Authorization
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}