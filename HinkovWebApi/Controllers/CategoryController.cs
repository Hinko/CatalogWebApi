namespace HinkovWebApi.Controllers
{
    using HinkovWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    public class CategoryController : ApiController
    {
        private static Random random = new Random();

        private const string URL = "www.google.com";
        private const string TmpData = "Lorem ipsuim dolores amet vin...";

        private string[] categoryIds = new string[] { "action", "drama", "killme" };

        private string[] actorNames = new string[] { "Vandam", "Pinokio", "Gonzalez", "Vatman" };

        private string[] titles = new string[] { "Make me hard", "Make me soft", "JailBreak", "WhiteNiggazzz" };

        private string[] year = new string[] { "1999", "2000", "2048", "3999", "1885" };

        private List<Category> categories = new List<Category>();

        private Dictionary<string, Category> catalog = new Dictionary<string, Category>();

        public CategoryController()
        {
            initCollection();
        }

        //public string Get(string id)
        //{
        //    return "value";
        //}

        public IHttpActionResult Post(string id)
        {
            if (this.catalog.ContainsKey(id))
            {
                return Ok(this.catalog[id]);
            }

            return NotFound();
        }

        private void initCollection()
        {
            for (int i = 0; i < this.categoryIds.Length; i++)
            {
                var currentCategory = new Category();

                currentCategory.Url = URL;
                currentCategory.TmpData = TmpData;

                var movieCount = random.Next(2, 10);

                for (int j = 0; j < movieCount; j++)
                {
                    var currentMovie = new Movie();

                    currentMovie.Actor = this.actorNames[j % this.actorNames.Length];
                    currentMovie.Title = this.titles[j % this.titles.Length];
                    currentMovie.Year = this.year[j % this.year.Length];

                    currentCategory.Movies.Add(currentMovie);
                }

                this.catalog[this.categoryIds[i]] = currentCategory;

            }
        }
    }
}
