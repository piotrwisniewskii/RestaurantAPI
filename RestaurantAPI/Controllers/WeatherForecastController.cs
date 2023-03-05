using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger ,IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery]int count, [FromBody]TemperatureRequest request)
        {
            if (count < 0 || request.TempMax < request.TempMin)
            {
                return BadRequest();
            }

            var result = _service.Get(count, request.TempMin, request.TempMax);
            return Ok(result);

        }

    }
}