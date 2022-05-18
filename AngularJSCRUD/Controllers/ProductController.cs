using AngularJSCRUD.Models;
using AngularJSCRUD.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJSCRUD.Controllers
{
    public class ProductController : Controller
    {
        private ProductService objCust;
        public ProductController()
        {
            this.objCust = new ProductService();
        }

        // GET: Category
        public ActionResult Product()
        {
            return View();
        }

        // GET: All Product
        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10; IEnumerable<object> customers = null;
            try
            {
                object[] parameters = { Count };
                customers = objCust.GetAll(parameters);
            }
            catch (Exception exc)
            {
                string msg = exc.Message;
            }
            return Json(customers.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Get Single Product
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            object customer = null;
            try
            {
                object[] parameters = { id };
                customer = this.objCust.GetbyID(parameters);
            }
            catch { }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Insert()
        {
            return View();
        }

        // POST: Save New Category
        [HttpPost]
        public JsonResult Insert(Product model)
        {
            Int64 result = 0; bool status = false;
            if (ModelState.IsValid || model.IdCategory > 0)
            {
                try
                {
                    object[] parameters = { model.DesProduct, model.DesUrl == null ? "" : model.DesUrl, model.IdCategory };
                    result = objCust.Insert(parameters);
                    if (result >= 1)
                    {
                        status = true;
                    }
                    return Json(new { success = status });
                }
                catch { }
            }
            return Json(new
            {
                success = false,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray()
            });
        }

        public ActionResult Update()
        {
            return View();
        }

        // POST: Update Existing Product
        [HttpPost]
        public JsonResult Update(Product model)
        {
            Int64 result = 0; bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = { model.Id, model.DesProduct, model.DesUrl == null ? "" : model.DesUrl, model.IdCategory };
                    result = objCust.Update(parameters);
                    if (result >= 1)
                    {
                        status = true;
                    }
                    return Json(new { success = status });
                }
                catch { }
            }
            return Json(new
            {
                success = false,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray()
            });
        }

        // DELETE: Delete Product
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            Int64 result = 0; bool status = false;
            try
            {
                object[] parameters = { id };
                result = objCust.Delete(parameters);
                if (result == 1)
                {
                    status = true;
                }
            }
            catch { }
            return Json(new
            {
                success = status
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}