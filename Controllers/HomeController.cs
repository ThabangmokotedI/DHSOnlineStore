using DHSOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DHSOnlineStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}


        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult Services() => View();
        public IActionResult Contact() => View();

        
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
