using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microphone;
using Refit;

namespace Blogs.Controllers
{
    public class HomeController : Controller
    {

        IClusterClient client { get; set; }
        public IActionResult Index([FromServices]IClusterClient client)
        {
            // Get all available services registred under the "article" name.
            var availablesServices = client.GetServiceInstancesAsync("articles").Result;
            // Get one of theses.
            var usedService = availablesServices.First();
            // refit : creating a API for this endpoint.
            var microService = RestService.For<IArticlesAPI>(string.Format("http://{0}:{1}", usedService.Host, usedService.Port));
            // get the data
            var data = microService.GetData().Result;
            //join it and send it to the page.
            ViewData["Message"] = string.Join(",", data.Select(x => x.Title));
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
    }

    public interface IArticlesAPI
    {
        [Get("/api/articles")]
        Task<IEnumerable<Article>> GetData();
    }

    public class Article
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
