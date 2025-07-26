using System;
using Ecom.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Ecom.DataAccess.Repository.IRepository;
using FirstProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecom.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public  void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=> u.Id == product.Id);
            if (product != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Author = product.Author;
                objFromDb.Description = product.Description;
                objFromDb.ISBN = product.ISBN;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.CategoryId = product.CategoryId;
                if (product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }
            }
            
        }

        
    }
}
