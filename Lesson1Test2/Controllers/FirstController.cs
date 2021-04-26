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
            return Ok(ValuesHolder.AddDataHolder(input).ToString());
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(ValuesHolder.Values.ToString());
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string dateToUpdate, [FromQuery] string newValue)
        {
            return Ok(ValuesHolder.UpdateForecast(dateToUpdate, newValue).ToString());
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            return Ok(ValuesHolder.DeleteForecast(stringsToDelete).ToString());
        }

        [HttpGet]
        public string Get()
        {
            return "JustUnswer";
        }
    }

}
