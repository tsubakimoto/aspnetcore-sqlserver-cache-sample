using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Cache()
        {
            var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            var value = await _cache.GetAsync(nameof(now).ToLower());
            if (value == null)
            {
                await _cache.SetAsync(nameof(now).ToLower(), Encoding.UTF8.GetBytes(now));
            }
            else
            {
                now = Encoding.UTF8.GetString(value);
            }

            return Content($"じこく→ {now}");
        }

        public IActionResult Session()
        {
            var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            var key = $"{nameof(Session).ToLower()}_{nameof(now).ToLower()}";
            var value = HttpContext.Session.GetString(key);
            if (value == null)
            {
                HttpContext.Session.SetString(key, now);
            }
            else
            {
                now = value;
            }
            return Content($"じこく→ {now}");
        }

        public IActionResult Session2()
        {
            var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            var key = $"{nameof(Session2).ToLower()}_{nameof(now).ToLower()}";
            var value = TempData[key];
            if (value == null)
            {
                TempData[key] = now;
            }
            else
            {
                now = value.ToString();
            }
            return Content($"じこく→ {now}");
        }

        public IActionResult ViewBag1()
        {
            var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");
            var value = ViewBag.ViewBag1now; //ViewBagは次のリクエストに引き継がれない
            if (value == null)
            {
                ViewBag.ViewBag1now = now;
            }
            else
            {
                now = value.ToString();
            }
            return Content($"じこく→ {now}");
        }
    }
}
