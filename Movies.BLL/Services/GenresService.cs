using Microsoft.EntityFrameworkCore;
using Movies.BLL.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.BLL.Services
{
    public class GenresService : IGenresService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GenresService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _unitOfWork.Repository<Genre>()
                                     .GetAllAsync(g => g.Name);
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Genre>().GetByIdAsync(id);
        }

        public async Task<bool> IsValidGenreAsync(int id)
        {
            return await _unitOfWork.Repository<Genre>().AnyAsync(g => g.ID == id);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            await _unitOfWork.Repository<Genre>().AddAsync(genre);
            await _unitOfWork.Complete();
            return genre;
        }

        public async Task<Genre> UpdateAsync(Genre genre)
        {
            _unitOfWork.Repository<Genre>().Update(genre);
            await _unitOfWork.Complete();
            return genre;
        }

        public async Task<bool> DeleteAsync(int id)
        {
          return await _unitOfWork.Repository<Genre>().DeleteAsync(id);
        }

    }
}
