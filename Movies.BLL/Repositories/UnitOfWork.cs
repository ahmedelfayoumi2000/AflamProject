using Movies.BLL.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repostories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        //Save Changes
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        // Delete or Releases the allocated resources this Context Or EF booked
        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repostories == null)
                _repostories = new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repostories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repostories.Add(type, repository);
            }
            return (IGenericRepository<TEntity>)_repostories[type];
        }
    }
}
