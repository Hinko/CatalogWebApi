using System.Collections.Generic;

namespace HinkovWebApi.Models
{
    public class Category
    {
        public Category()
        {
            this.Movies = new List<Movie>();
        }

        public string Url { get; set; }

        public string TmpData { get; set; }

        public List<Movie> Movies { get; set; }
    }
}