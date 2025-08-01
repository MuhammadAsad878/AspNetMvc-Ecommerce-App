﻿using Ecom.Models;
using System;
using Ecom.DataAccess.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.DataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
