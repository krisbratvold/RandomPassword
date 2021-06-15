using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPassword.Models;

namespace RandomPassword.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("count") == null)
            {
            HttpContext.Session.SetInt32("count", 1);
            }
            else
            {
                int count = HttpContext.Session.GetInt32("count").Value;
                HttpContext.Session.SetInt32("count", count + 1);
            }
            ViewBag.Count = HttpContext.Session.GetInt32("count");
            Random rand = new Random();
            string choices = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string password = "";
            for (int i = 0; i < 14; i++)
            {
                password += choices[rand.Next(0,36)];
            }
            ViewBag.Password = password;
            return View("Index");
        }
        [HttpGet("/clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
