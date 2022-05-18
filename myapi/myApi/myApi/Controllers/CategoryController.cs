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
    [ApiExplorerSettings(GroupName = "OpenAPISpecificationCategory")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryContext _context;

        public CategoryController(CategoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all category details
        /// </summary>
        /// <returns>All category details with id, descategory and codecategory fields</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                List<classes.Category> lCategory = Model.Category.getAllCategory();
                if (lCategory.Count > 0)
                {
                    foreach (var entity in _context.Categorys)
                        _context.Categorys.Remove(entity);

                    _context.SaveChanges();
                    _context.Categorys.AddRange(lCategory.ToList());
                    _context.SaveChanges();
                }
            }
            catch
            { }
            var customers = _context.Categorys.ToList();

            return Ok(customers);
        }


        /// <summary>
        /// Get category detail by id
        /// </summary>
        /// <param name="idcategory">This id is unique/primary key of category </param>
        /// <returns>Category category details with id, descategory and codecategory fields</returns>
        [HttpGet]
        [Route("{idcategory}", Name = "GetCategory")]
        public ActionResult<Category> Get(Int64 idcategory)
        {
            var customers = _context.Categorys.Find(idcategory);

            if (customers == null)
            {
                NotFound();
            }

            return Ok(customers);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            Int64 _id = Model.Category.insertCategory(category);
            if (_id > 0)
            {
                category.Id = _id;
                _context.Categorys.Add(category);
                _context.SaveChanges();

                return CreatedAtRoute("GetCategory", new { idcategory = category.Id }, category);
            }
            else
            {
                return NotFound();
            }

        }

        // PUT api/<CategoryController>/5
        [HttpPut("{idcategory}")]
        public IActionResult Put(Int64 idcategory, [FromBody] Category category)
        {

            var categorys = _context.Categorys.First(a => a.Id == idcategory);

            if (categorys == null)
            {
                return NotFound();
            }
            category.Id = idcategory;
            if (Model.Category.updateCategory(category))
            {
                // TODO- Use AutoMapper
                categorys.DesCategory = category.DesCategory;
                categorys.CodeCategory = category.CodeCategory;

                _context.Categorys.Update(categorys);
                _context.SaveChanges();

                return Ok(categorys);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete]
        [Route("{categoryId}")]
        public ActionResult<Customer> Delete(Int64 categoryId)
        {
            var categorys = _context.Categorys.First(a => a.Id == categoryId);

            if (categorys == null)
            {
                return NotFound();
            }
            if (Model.Category.deleteCategory(categorys))
            {
                _context.Categorys.Remove(categorys);
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
