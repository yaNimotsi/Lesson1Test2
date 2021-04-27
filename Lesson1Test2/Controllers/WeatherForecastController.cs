using System;
using Microsoft.AspNetCore.Mvc;


namespace Lesson1Test2.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime dateForecast, [FromQuery] double forecast)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.AddDataHolder(dateForecast, forecast);
            return Ok(valuesHolder.MyToString());
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime sDateRange, [FromQuery] DateTime eDateRange)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.GetForecastByRange(sDateRange, eDateRange);
            return Ok(valuesHolder.MyToString());
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime dateToUpdate, [FromQuery] double newVal)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.UpdateForecast(dateToUpdate, newVal);
            return Ok(valuesHolder.MyToString());
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime sDateForDelete, [FromQuery] DateTime eDateToDelete)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.DeleteForecast(sDateForDelete, eDateToDelete);
            return Ok(valuesHolder.MyToString());
        }

        [HttpGet]
        public string Get()
        {
            return "JustAnswer";
        }
    }

}
