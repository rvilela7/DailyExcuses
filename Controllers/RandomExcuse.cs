﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DailyExcuses.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class RandomExcuse : ControllerBase
{
    private const string excusesENFile = "excusesEN.txt";
    private const string excusesPTFile = "excusesPT.txt";
    private readonly ILogger<RandomExcuse> _logger;

    public RandomExcuse(ILogger<RandomExcuse> logger)
    {
        _logger = logger;
    }

    [HttpGet("/quote")]
    [HttpGet("/excuses")]
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
            case "en":
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

