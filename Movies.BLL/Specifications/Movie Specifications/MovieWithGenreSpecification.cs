using Movies.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Specifications.Movie_Specifications
{
    public class MovieWithGenreSpecification : BaseSpecification<Movie>
    {
        public MovieWithGenreSpecification()
        {
            AddInclude(m => m.genre);
            AddOrderByDescending(m => m.Rate);
        }
        public MovieWithGenreSpecification(int id) : base(p => p.ID == id)
        {
            AddInclude(m => m.genre);
        }
    }
}
