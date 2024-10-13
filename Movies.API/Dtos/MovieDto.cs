using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos
{
    public class MovieDto : BaseMovieEntity
    {
        public IFormFile Poster { get; set; }

        public int GenreID { get; set; }
    }
}
