using Microsoft.EntityFrameworkCore;
using myApi.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
      : base(options)
        {

        }

        public CustomerContext()
        {

        }
        public DbSet<Customer> Customers { get; set; }
    }
}
