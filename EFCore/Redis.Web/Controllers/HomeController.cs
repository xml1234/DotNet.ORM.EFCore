using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redis.Web.Models;
using StackExchange.Redis;

namespace Redis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public HomeController(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _db = _redis.GetDatabase();
        }

        public IActionResult Index()
        {
            _db.StringSet("fullName", "fullName1");

            var name = _db.StringGet("fullName");

            return View("Index", name);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
