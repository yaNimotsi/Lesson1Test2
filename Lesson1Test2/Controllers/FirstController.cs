using Microsoft.AspNetCore.Mvc;


namespace Lesson1Test2.Controllers
{
    [ApiController]
    [Route("api/crud")]
    public class FirstController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public FirstController(ValuesHolder holder)
        {
            this._holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] string input)
        {
            _holder.AddDataHolder(input);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Values?.ToString());
        }

        [HttpGet]
        public string Get()
        {

            return "JustUnswer";
        }
    }

}
