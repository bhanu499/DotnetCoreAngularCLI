using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

namespace netcoreangularcli.Controllers
{
    public class HomeController : Controller
    {
    private readonly IHostingEnvironment _env;
    public HomeController(IHostingEnvironment env)
    {
      
      _env = env;
    }
    public async Task<IActionResult> Index()
        {
          ViewBag.MainDotJs = await GetMainDotJs();
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
            return View();
        }
        public async Task<string> GetMainDotJs()
        {
          var basePath = _env.WebRootPath;

          if (_env.IsDevelopment() && !System.IO.File.Exists(basePath + "main.bundle.js"))
          {
            // Just a .js request to make it wait to finish webpack dev middleware finish creating bundles:
            // More info here: https://github.com/aspnet/JavaScriptServices/issues/578#issuecomment-272039541
            using (var client = new HttpClient())
            {
              var requestUri = Request.Scheme + "://" + Request.Host + "/dist/main.bundle.js";
              await client.GetAsync(requestUri);
            }
          }

          var info = new System.IO.DirectoryInfo(basePath);
          var file = info.GetFiles()
               .Where(f => _env.IsDevelopment() ? f.Name == "main.bundle.js" : f.Name.StartsWith("main.") && !f.Name.EndsWith("bundle.map"));
          return file.FirstOrDefault().Name;
        }
  }
}
