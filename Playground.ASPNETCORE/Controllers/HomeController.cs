using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Playground.ASPNETCORE.Models;

namespace Playground.ASPNETCORE.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _env;
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine(System.IO.Path.Combine(_env.WebRootPath, "uploads", "img"));
            return View(new Profile());
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
            return View();
        }
    }
}
