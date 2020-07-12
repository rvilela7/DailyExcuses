using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyExcuses.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class RandomExcuse : ControllerBase
    {
        private string excusesFile = "excuses.txt";
        private readonly ILogger<RandomExcuse> _logger;

        public RandomExcuse(ILogger<RandomExcuse> logger)
        {
            _logger = logger;
        }

        [HttpGet("/")]
        public string Get()
        {
            string[] excuses = System.IO.File.ReadAllLines(excusesFile);
            Random rnd = new Random();
            int i = rnd.Next(excuses.Count());
            return excuses[i];
        }

        [HttpPost("/New")]
        public IActionResult InsertNew([FromBody] string excuse)
        {   
            if (string.IsNullOrWhiteSpace(excuse))
                return BadRequest("Requires valid excuse!");

            System.IO.File.AppendAllText(excusesFile, excuse.Trim() + Environment.NewLine);
            return Ok(excuse);
        }

        [HttpGet("/List")]
        public List<string> GetList()
        {
            string[] excuses = System.IO.File.ReadAllLines(excusesFile);
            return excuses.ToList();
        }
    }
}
