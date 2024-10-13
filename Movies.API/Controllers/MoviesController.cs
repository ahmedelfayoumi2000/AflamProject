using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos;
using Movies.API.Errors;
using Movies.BLL.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Forma.API.Errors;

namespace Movies.API.Controllers
{

    public class MoviesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IGenresService _genresService;
        private readonly IMoviesService _moviesService;


        private new List<string> _allowedextenstions = new List<string> { ".jpg", ".jpeg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMapper mapper,
            IGenresService genresService,
            IMoviesService moviesService
           )
        {
            _mapper = mapper;
            _genresService = genresService;
            _moviesService = moviesService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMovices()
        {
            var movies = await _moviesService.GetAll();
            var data = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMovieById(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie is null) return NotFound(new ApiResponse(404, $"Movie with ID {id} not found."));
            var data = _mapper.Map<Movie, MovieDetailsDto>(movie);
            return Ok(data);

        }


        [HttpPost]
        public async Task<ActionResult> CreateMovie([FromForm] MovieDto dto)
        {
            if (!_allowedextenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Only .png, .jpg and .jpeg are allowed!" }
                });
            }

            if (dto.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Max allowed size for poster is 1MB!" }
                });
            }

            var isValidGenre = await _genresService.IsValidGenreAsync(dto.GenreID);
            if (!isValidGenre)
            {
                return BadRequest(new ApiValidationErrorResponse
                {
                    Errors = new[] { "Invalid genre ID!" }
                });
            }

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<MovieDto, Movie>(dto);
            movie.Poster = dataStream.ToArray();

            var createdMovie = await _moviesService.Add(movie);

            var createdMovieDto = _mapper.Map<Movie, MovieDetailsDto>(createdMovie);

            return Ok(createdMovieDto);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] MovieDto dto)
        {
            var movie = await _moviesService.GetById(id);

            if (movie is null)
                return NotFound(new ApiResponse(404, $"No movie was found with ID {id}"));

            var isValidGenre = await _genresService.IsValidGenreAsync(dto.GenreID);

            if (!isValidGenre)
                return BadRequest(new ApiResponse(400, "Invalid genre ID!"));

            if (dto.Poster != null)
            {
                if (!_allowedextenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest(new ApiResponse(400, "Only .png, .jpg, and .jpeg images are allowed!"));

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest(new ApiResponse(400, "Max allowed size for poster is 1MB!"));

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                movie.Poster = dataStream.ToArray();
            }

            var updatedMovie = _mapper.Map(dto, movie);

            var updatedMovieEntity = await _moviesService.Update(updatedMovie);

            var updatedMovieDto = _mapper.Map<Movie, MovieDetailsDto>(updatedMovieEntity);

            return Ok(updatedMovieDto);

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var result = await _moviesService.Delete(id);
            if (!result) return NotFound(new ApiResponse(404, $"Movie with ID {id} not found."));

            return Ok(new ApiResponse(200, $"Movie with ID {id} deleted successfully."));
        }



    }
}
