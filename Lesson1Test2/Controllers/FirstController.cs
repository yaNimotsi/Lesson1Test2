using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace Lesson1Test2.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class FirstController : ControllerBase
    {
        private List<DataHolder> _holder;
        

        public FirstController(ValuesHolder holder)
        {
            this._holder = ValuesHolder.Values;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {
            var _holder = ValuesHolder.AddDataHolder(input);
            return Ok(_holder?.ToString());
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder?.ToString());
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string dateToUpdate, [FromQuery] string newValue)
        {
            var _holder = ValuesHolder.UpdateForecast(dateToUpdate, newValue);

            return Ok(_holder?.ToString());
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            var _holder = ValuesHolder.DeleteForecast(stringsToDelete);
            return Ok(_holder?.ToString());
        }

        [HttpGet]
        public string Get()
        {
            return "JustUnswer";
        }
    }

}
