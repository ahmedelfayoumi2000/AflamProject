using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Specifications
{
    public class SpecificationsEvaiuator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);


            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);
            

            query = spec.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

            //context.Set<Product>().Include(p => p.ProductPrand).Include(p => p.ProductType).ToList();

            return query;
        }

    }
}
