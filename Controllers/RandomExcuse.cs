using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DailyExcuses.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class RandomExcuse : ControllerBase
{
    private const string excusesENFile = "data/excusesEN.txt";
    private const string excusesPTFile = "data/excusesPT.txt";
    private readonly ILogger<RandomExcuse> _logger;

    public RandomExcuse(ILogger<RandomExcuse> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns a ramdom excuse
    /// </summary>
    /// <param name="lang">Language code: 'en' for English, 'pt' for Portuguese.</param>
    /// <returns>Random excuse</returns>
    [HttpGet("/quote")]
    [HttpGet("/excuse")]
    public string Get(string lang = "en")
    {
        string[] excuses = getExcuses(lang);
        Random rnd = new Random();
        int i = rnd.Next(excuses.Count());
        return excuses[i];
    }

    [HttpPost("/new")]
    [Obsolete("This method is obsolete.")]
    public IActionResult InsertNew([FromBody] string excuse)
    {
        if (!Debugger.IsAttached)
        {
            throw new NotImplementedException("Unavailable in production");
        }

        if (string.IsNullOrWhiteSpace(excuse))
            return BadRequest("Requires valid excuse!");

        System.IO.File.AppendAllText(excusesPTFile, excuse.Trim() + Environment.NewLine);
        return Ok(excuse);
    }

    /// <summary>
    /// Get the list of available excuses in the specified language.
    /// </summary>
    /// <param name="language">Language code: 'en' for English, 'pt' for Portuguese.</param>
    /// <returns>List of available excuses.</returns>
    [HttpGet("/list")]
    [Produces("application/json")]
    public IActionResult GetList(string lang = "en")
    {
        string[] excuses = getExcuses(lang);
        return Ok(new JsonResult(excuses));
    }

    private string[] getExcuses(string lang)
    {
        switch (lang)
        {
            case "pt":
                return System.IO.File.ReadAllLines(excusesPTFile);
            default:
                return System.IO.File.ReadAllLines(excusesENFile);
        }
    }

    // Needs CORS and OPTIONS set in startup

    // [HttpOptions]
    // public IActionResult GetOptions()
    // {
    //     return Ok();
    // }
}

