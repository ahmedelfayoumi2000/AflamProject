using Microsoft.EntityFrameworkCore;
using Movies.BLL.Interfaces;
using Movies.BLL.Specifications;
using Movies.BLL.Specifications.Movie_Specifications;
using Movies.DAL.Data;
using Movies.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, object>> orderBy)
        {
            return await _context.Set<T>().OrderBy(orderBy).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationsEvaiuator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<T> GetByIdAsync(int id)
         => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
          => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
          => _context.Set<T>().Update(entity);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
         => _context.Set<T>().AnyAsync(predicate);

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

       
    }
}
