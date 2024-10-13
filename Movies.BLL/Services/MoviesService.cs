    using Movies.BLL.Interfaces;
    using Movies.BLL.Specifications.Movie_Specifications;
    using Movies.DAL.Data;
    using Movies.DAL.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Movies.BLL.Services
    {
        public class MoviesService : IMoviesService
        {
            private readonly IUnitOfWork _unitOfWork;

            public MoviesService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Movie>> GetAll(int genreId = 0)
            {
                var spec = new MovieWithGenreSpecification();
                return await _unitOfWork.Repository<Movie>().GetAllWithSpecAsync(spec);
            }

            public async Task<Movie> GetById(int id)
            {
                var spec = new MovieWithGenreSpecification(id);
                return await _unitOfWork.Repository<Movie>().GetByIdWithSpecAsync(spec);
            }
            public async Task<Movie> Add(Movie movie)
            {
                await _unitOfWork.Repository<Movie>().AddAsync(movie);

                await _unitOfWork.Complete();
                var spec = new MovieWithGenreSpecification(movie.ID);
                return await _unitOfWork.Repository<Movie>().GetByIdWithSpecAsync(spec);
            }

        public async Task<Movie> Update(Movie movie)
        {
            // تحديث الفيلم
            _unitOfWork.Repository<Movie>().Update(movie);

            // حفظ التغييرات
            await _unitOfWork.Complete();

            // إنشاء Specification لاسترجاع الفيلم بعد التحديث
            var spec = new MovieWithGenreSpecification(movie.ID);

            // الحصول على الفيلم باستخدام Specification
            var updatedMovie = await _unitOfWork.Repository<Movie>().GetByIdWithSpecAsync(spec);

            return updatedMovie;
        }


        public async Task<bool> Delete(int id)
            {
               return await _unitOfWork.Repository<Movie>().DeleteAsync(id);
            }

      

       
        }
    }
