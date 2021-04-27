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
            valuesHolder.AddForecast(dateForecast, forecast);
            return Ok(valuesHolder.MyToString());
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime startDateRange, [FromQuery] DateTime endDateRange)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.GetForecastByRange(startDateRange, endDateRange);
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
        public IActionResult Delete([FromQuery] DateTime startDateForDelete, [FromQuery] DateTime endDateToDelete)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.DeleteForecast(startDateForDelete, endDateToDelete);
            return Ok(valuesHolder.MyToString());
        }

        [HttpGet]
        public string Get()
        {
            return "JustAnswer";
        }
    }

}
