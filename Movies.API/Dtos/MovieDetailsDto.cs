namespace Movies.API.Dtos
{
    public class MovieDetailsDto : BaseMovieEntity
    {
        public int Id { get; set; }

        public byte[] Poster { get; set; }

        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}
