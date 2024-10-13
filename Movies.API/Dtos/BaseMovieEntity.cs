using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos
{
    public class BaseMovieEntity
    {

        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoreLine { get; set; }
    }
}
