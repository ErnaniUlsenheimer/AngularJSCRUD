using Microsoft.EntityFrameworkCore;
using myApi.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
     : base(options)
        {

        }

        public ProductContext()
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
