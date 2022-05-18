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
    [ApiExplorerSettings(GroupName = "OpenAPISpecificationProduct")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all category details
        /// </summary>
        /// <returns>All category details with id, descategory and codecategory fields</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                List<classes.Product> lProduct = Model.Product.getAllProduct();
                if (lProduct.Count > 0)
                {
                    foreach (var entity in _context.Products)
                        _context.Products.Remove(entity);

                    _context.SaveChanges();
                    _context.Products.AddRange(lProduct.ToList());
                    _context.SaveChanges();
                }
            }
            catch
            { }
            var customers = _context.Products.ToList();

            return Ok(customers);
        }


        /// <summary>
        /// Get product detail by id
        /// </summary>
        /// <param name="idproduct">This id is unique/primary key of product </param>
        /// <returns>Product details with id and fields</returns>
        [HttpGet]
        [Route("{idproduct}", Name = "GetProduct")]
        public ActionResult<Product> Get(Int64 idproduct)
        {
            var customers = _context.Products.Find(idproduct);

            if (customers == null)
            {
                NotFound();
            }

            return Ok(customers);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            Int64 _id = Model.Product.insertProduct(product);
            if (_id > 0)
            {
                product.Id = _id;
                _context.Products.Add(product);
                _context.SaveChanges();

                return CreatedAtRoute("GetProduct", new { idproduct = product.Id }, product);
            }
            else
            {
                return NotFound();
            }

        }

        // PUT api/<ProductController>/5
        [HttpPut("{idproduct}")]
        public IActionResult Put(Int64 idproduct, [FromBody] Product product)
        {

            var products = _context.Products.First(a => a.Id == idproduct);

            if (products == null)
            {
                return NotFound();
            }
            products.Id = idproduct;
            if (Model.Product.updateProduct(product))
            {
                // TODO- Use AutoMapper
                products.DesProduct = product.DesProduct;
                products.DesUrl = product.DesUrl;
                products.IdCategory = product.IdCategory;

                _context.Products.Update(products);
                _context.SaveChanges();

                return Ok(products);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete]
        [Route("{productId}")]
        public ActionResult<Customer> Delete(Int64 productId)
        {
            var products = _context.Products.First(a => a.Id == productId);

            if (products == null)
            {
                return NotFound();
            }
            if (Model.Product.deleteProduct(products))
            {
                _context.Products.Remove(products);
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
