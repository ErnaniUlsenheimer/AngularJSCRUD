using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSCRUD.Models
{
    public class Category
    {
        public Int64 Id { get; set; }

        /// <summary>
        /// Name of Category
        /// </summary>
        public string DesCategory { get; set; }

        /// <summary>
        /// code of Customer
        /// </summary>
        public string CodeCategory { get; set; }
    }
}