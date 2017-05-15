using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MicroServicesArticles.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {

        [HttpGet]
        public IEnumerable<Article> Get()
        {
            var list = new List<Article>()
            {
                new Article(){Title = "Article 1", Text = "Some text"},
                  new Article(){Title = "Article 2", Text = "Lorem ipsum"},
                    new Article(){Title = "Article 42", Text = "The answer"},
            };
            return list;
        }
    }


    public class Article
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
