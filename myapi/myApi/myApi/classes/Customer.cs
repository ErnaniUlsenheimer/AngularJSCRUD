using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.classes
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
