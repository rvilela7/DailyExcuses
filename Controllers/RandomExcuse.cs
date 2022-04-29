using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DailyExcuses.Controllers
{
    [ApiController]
    // [Route("[controller]")]
    public class RandomExcuse : ControllerBase
    {
        private string excusesFile = "excuses.txt";
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
        public IActionResult GetList()
        {
            string[] excuses = System.IO.File.ReadAllLines(excusesFile);
            return Ok(JsonConvert.SerializeObject(excuses.ToList()));
        }

        // Needs CORS and OPTIONS set in startup

        // [HttpOptions]
        // public IActionResult GetOptions()
        // {
        //     return Ok();
        // }
    }
}
