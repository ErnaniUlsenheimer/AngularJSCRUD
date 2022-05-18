using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSCRUD.Models
{
    public class ProductCategory
    {
        public Product product { get; set; }
        public Category category { get; set; }
    }
}