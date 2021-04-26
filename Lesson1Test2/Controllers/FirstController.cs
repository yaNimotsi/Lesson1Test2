using Microsoft.AspNetCore.Mvc;


namespace Lesson1Test2.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class FirstController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.AddDataHolder(input);
            return Ok(valuesHolder.MyToString());
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            var valuesHolder = new ValuesHolder();
            return Ok(valuesHolder.MyToString());
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string dateToUpdate, [FromQuery] string newValue)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.UpdateForecast(dateToUpdate, newValue);
            return Ok(valuesHolder.MyToString());
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            var valuesHolder = new ValuesHolder();
            valuesHolder.DeleteForecast(stringsToDelete);
            return Ok(valuesHolder.MyToString());
        }

        [HttpGet]
        public string Get()
        {
            return "JustAnswer";
        }
    }

}
