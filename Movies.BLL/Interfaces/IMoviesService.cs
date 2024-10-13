using Movies.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Interfaces
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(int genreId = 0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
       Task <Movie> Update(Movie movie);
        Task<bool> Delete(int id);
    }
}
