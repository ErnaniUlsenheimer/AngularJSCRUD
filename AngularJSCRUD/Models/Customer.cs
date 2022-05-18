using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSCRUD.Models
{
    public class Customer
    {
        public Int64 Id { get; set; }

        /// <summary>
        /// Name of Customer
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// code of Customer
        /// </summary>
        public string CustomerCode { get; set; }
    }
}