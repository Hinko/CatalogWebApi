namespace HinkovWebApi.Controllers
{
    using HinkovWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web.Http;

    using System.Linq;

    using Newtonsoft.Json;

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

        private List<Movie> Movies = new List<Movie>();

        private Dictionary<string, Category> catalog = new Dictionary<string, Category>();

        public CategoryController()
        {
            if (this.Movies.Count == 0)
                loadMovies(Properties.Settings.Default.EndPoint);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]CategoryAccept category)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest("Ian says Invalid Body :(");

            return  Ok(this.Movies.Where(m => m.Category.ToLower() == category.Category.ToLower()));

        }


        private void loadMovies(string endPoint)
        {
            HttpWebRequest request = CreateWebRequest(endPoint);
            List<Movie> allMovies;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }



                // grab the response  
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseValue = reader.ReadToEnd();
                        allMovies = JsonConvert.DeserializeObject<List<Movie>>(responseValue);
                    }
                }
            }

            this.Movies.AddRange(allMovies);
        }

        private static HttpWebRequest CreateWebRequest(string endPoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "text/json";

            return request;
        }
    }
}
