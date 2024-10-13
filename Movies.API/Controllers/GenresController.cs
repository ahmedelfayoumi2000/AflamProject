using AutoMapper;
using Forma.API.Errors;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Dtos;
using Movies.API.Errors;
using Movies.BLL.Interfaces;
using Movies.BLL.Services;
using Movies.DAL.Entity;
using System.Threading.Tasks;

namespace Movies.API.Controllers
{
    public class GenresController : BaseApiController
    {
        private readonly IGenresService _genresService;
        private readonly IMapper _mapper;

        public GenresController(IGenresService genresService, IMapper mapper)
        {
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAllAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(GenreDto dto)
        {
            try
            {
                var genre = _mapper.Map<GenreDto, Genre>(dto);
                await _genresService.AddAsync(genre);
                return Ok(genre);
            }
            catch (AutoMapperMappingException)
            {
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "A Problem Occured With Creating Genre" } });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] GenreDto dto)
        {
            var genre = await _genresService.GetByIdAsync(id);
            if (genre is null) return NotFound(new ApiResponse(404, $"No genre was found with ID: {id}"));


            _mapper.Map(dto, genre);

            var updateGenre = await _genresService.UpdateAsync(genre);

            return Ok(updateGenre);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _genresService.DeleteAsync(id);
            if (!result) return NotFound(new ApiResponse(404, $"Movie with ID {id} not found."));

            return Ok(new ApiResponse(200, $"Genre with ID {id} deleted successfully."));
        }
    }
}
