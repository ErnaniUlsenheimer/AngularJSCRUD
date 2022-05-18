using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSCRUD.Models
{
    public class Product
    {
        public Int64 Id { get; set; }

        public string DesProduct { get; set; }

        public string DesUrl { get; set; }

        public Int64 IdCategory { get; set; }
    }
}