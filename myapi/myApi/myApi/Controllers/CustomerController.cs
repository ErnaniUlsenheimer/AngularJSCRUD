using Microsoft.AspNetCore.Mvc;
using myApi.classes;
using myApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "OpenAPISpecificationCustomer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all customer details
        /// </summary>
        /// <returns>All customer details with id, customername and cutomer code fields</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                List<classes.Customer> lCustomer = Model.Customer.getAllCustomer();
                if (lCustomer.Count > 0)
                {
                    foreach (var entity in _context.Customers)
                        _context.Customers.Remove(entity);

                    _context.SaveChanges();
                    _context.Customers.AddRange(lCustomer.ToList());
                    _context.SaveChanges();
                }
            }
            catch
            { }
            var customers = _context.Customers.ToList();

            return Ok(customers);
        }


        /// <summary>
        /// Get customer detail by id
        /// </summary>
        /// <param name="id">This id is unique/primary key of customer </param>
        /// <returns>Customer details with id, customername and cutomer code fields</returns>
        [HttpGet]
        [Route("{id}", Name = "GetCustomer")]
        public ActionResult<Customer> Get(Int64 id)
        {
            var customers = _context.Customers.Find(id);

            if (customers == null)
            {
                NotFound();
            }

            return Ok(customers);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            Int64 _id = Model.Customer.insertCustomer(customer);
            if (_id > 0)
            {
                customer.Id = _id;
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
            }
            else
            {
                return NotFound();
            }

        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Int64 id, [FromBody] Customer customer)
        {
           
            var customers = _context.Customers.First(a => a.Id == id);

            if (customers == null)
            {
                return NotFound();
            }
            customer.Id = id;
            if (Model.Customer.updateCustomer(customer))
            {
                // TODO- Use AutoMapper
                customers.CustomerCode = customer.CustomerCode;
                customers.CustomerName = customer.CustomerName;

                _context.Customers.Update(customers);
                _context.SaveChanges();

                return Ok(customers);
            }
            else
            {
               return  NotFound();
            }
            
        }

        [HttpDelete]
        [Route("{customerId}")]
        public ActionResult<Customer> Delete(Int64 customerId)
        {
            var customers = _context.Customers.First(a => a.Id == customerId);

            if (customers == null)
            {
                return NotFound();
            }
            if (Model.Customer.deleteCustomer(customers))
            {
                _context.Customers.Remove(customers);
                _context.SaveChanges();

                return NoContent();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
