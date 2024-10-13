using System.ComponentModel.DataAnnotations;

namespace Movies.API.Dtos
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
