using Ecom.DataAccess.Repository.IRepository;
using FirstProject.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //db.categories == dbset;
            // we can also use multiple include statements  
            //_db.Products.Include(u => u.Category);

        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;       // If you want to include related entities, you can use Include method here
            query = query.Where(filter);       // For example: query = query.Include("RelatedEntityName");

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties
                             .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Dynamically include each navigation property (e.g., "Author", "Category")
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();    // If you want to order the results, you can use OrderBy method here
                                                          // For example: query = query.OrderBy(e => e.PropertyName);
        }

        /// <summary>
        /// Retrieves all records of type T from the database, optionally including related navigation properties.
        /// </summary>
        /// <param name="includeProperties">
        /// A comma-separated list of related entities (navigation properties) to include in the result.
        /// Example: "Category,Author"
        /// If null or empty, only the main entity will be loaded (no related data).
        /// </param>
        /// <returns>
        /// An IEnumerable<T> containing all the entities from the database, including the specified related data (if any).
        /// </returns>
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            // Start with the base query for the entity (e.g., Books)
            IQueryable<T> query = dbSet;

            // If related properties are specified, loop through and apply Include() for each one
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties
                             .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // Dynamically include each navigation property (e.g., "Author", "Category")
                    query = query.Include(includeProperty);
                }
            }

            // Execute the query and return the results as a list
            return query.ToList();
        }



        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

       
    }
}
