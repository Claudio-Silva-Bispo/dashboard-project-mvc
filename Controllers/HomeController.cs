using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BIProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace BIProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {   
    //     return View();
    // }

    public IActionResult Index()
    {

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {

            return RedirectToAction("Hero");
        }

        return View();
    }
    
    [Authorize]
    public IActionResult Hero()
    {   
        return View();
    }

    public IActionResult Conceitos()
    {
        return View();
    }

    public IActionResult Apoio()
    {
        return View();
    }
   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}

