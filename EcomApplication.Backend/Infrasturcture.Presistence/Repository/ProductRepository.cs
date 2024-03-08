using Application.Interface.Repository;
using Domain.Entity;
using Infrasturcture.Presistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.Presistence.Repository
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        private readonly DbSet<Product> products;
        public ProductRepository(ApplicationContext application):base(application)
        {
            this.products=application.Set<Product>();
        }
        
    }
}
