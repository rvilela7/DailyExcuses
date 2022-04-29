using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DailyExcuses.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class RandomExcuse : ControllerBase
    {
        private const string excusesFile = "excuses.txt";
        private readonly ILogger<RandomExcuse> _logger;

        public RandomExcuse(ILogger<RandomExcuse> logger)
        {
            _logger = logger;
        }

        [HttpGet("/quote")]
        public string Get()
        {
            string[] excuses = System.IO.File.ReadAllLines(excusesFile);
            Random rnd = new Random();
            int i = rnd.Next(excuses.Count());
            return excuses[i];
        }

        [HttpPost("/new")]
        [Obsolete("Not really tested")]
        public IActionResult InsertNew([FromBody] string excuse)
        {   
            if (string.IsNullOrWhiteSpace(excuse))
                return BadRequest("Requires valid excuse!");

            System.IO.File.AppendAllText(excusesFile, excuse.Trim() + Environment.NewLine);
            return Ok(excuse);
        }

        [HttpGet("/list")]
        [Produces("application/json")]
        public IActionResult GetList()
        {
            string[] excuses = System.IO.File.ReadAllLines(excusesFile);
            return Ok(new JsonResult(excuses));
        }

        // Needs CORS and OPTIONS set in startup

        // [HttpOptions]
        // public IActionResult GetOptions()
        // {
        //     return Ok();
        // }
    }
}
