using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DailyExcuses.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomExcuse : ControllerBase
    {
        private string excusesFile = "excuses.txt";
        private readonly ILogger<RandomExcuse> _logger;

        public RandomExcuse(ILogger<RandomExcuse> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            string[] excuses = System.IO.File.ReadAllLines(excusesFile);

            Random rnd = new Random();
            int i = rnd.Next(excuses.Count());
            return excuses[i];
        }
    }
}
