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
    public string GetQuote(string? lang)
    {
        string[] excuses = getExcuses(lang ?? "en");
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
    public IActionResult GetList(string? lang)
    {
        string[] excuses = getExcuses(lang ?? "en");
        return Ok(new JsonResult(excuses));
    }

    private string[] getExcuses(string lang)
    {
        return lang switch
        {
            "pt" => System.IO.File.ReadAllLines(excusesPTFile),
            _ => System.IO.File.ReadAllLines(excusesENFile),
        };
    }
}

