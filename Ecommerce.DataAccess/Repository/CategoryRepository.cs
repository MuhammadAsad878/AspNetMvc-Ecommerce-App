using Ecom.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Models;
using System.Linq.Expressions;
using FirstProject.Data;

namespace Ecom.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)  : base(db) 
        {
            _db = db;
        }




        public void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
