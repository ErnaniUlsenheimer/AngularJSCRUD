using Microsoft.EntityFrameworkCore;
using myApi.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Data
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options)
     : base(options)
        {

        }

        public CategoryContext()
        {

        }
        public DbSet<Category> Categorys { get; set; }
    }
}
