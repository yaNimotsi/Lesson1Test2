using Microsoft.AspNetCore.Mvc;

using System.Linq;

namespace Lesson1Test2.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class FirstControler : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public FirstControler(ValuesHolder holder)
        {
            this._holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {
            _holder.Values.Add(input);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Values);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] string stringsToUpdate, [FromQuery] string newValue)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i] == stringsToUpdate)
                    _holder.Values[i] = newValue;
            }

            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] string stringsToDelete)
        {
            _holder.Values = _holder.Values.Where(w => w != stringsToDelete).ToList();
            return Ok();
        }

        [HttpGet]
        public string Get()
        {

            return "JustUnswer";
        }
    }

}
