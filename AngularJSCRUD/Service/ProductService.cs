using AngularJSCRUD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace AngularJSCRUD.Service
{
    public class ProductService
    {
        private List<ProductCategory> CustRepository;

        public ProductService()
        {
            this.CustRepository = new List<ProductCategory>();
        }

        public IEnumerable<ProductCategory> GetAll(object[] parameters)
        {
            List<ProductCategory> lProductCategory = new List<ProductCategory>();
            try
            {
                lProductCategory = getAllProductCategory();
            }
            catch
            { }
            return lProductCategory.OrderBy(o=>o.category.Id).ThenBy(o=>o.product.DesProduct).ToList().ToArray();

        }

        private List<ProductCategory> getAllProductCategory()
        {
            List<ProductCategory> listProductCategory = new List<ProductCategory>();
            List<Category> listCategory = new List<Category>();
            List<Product> listProduct = new List<Product>();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Category");

                string text = "";
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "GET";

                var responseEnd = (HttpWebResponse)request.GetResponse();
                if (responseEnd.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text != null && text.Length > 1)
                {
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    listCategory = json_serializer.Deserialize<List<Category>>(text);
                }
                responseEnd.Close();


            }
            catch
            { }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Product");

                string text = "";
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "GET";

                var responseEnd = (HttpWebResponse)request.GetResponse();
                if (responseEnd.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text != null && text.Length > 1)
                {
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    listProduct = json_serializer.Deserialize<List<Product>>(text);
                }
                responseEnd.Close();


            }
            catch
            { }

            foreach(Product prod in listProduct)
            {
                ProductCategory obj = new ProductCategory();
                obj.product = prod;
                obj.category = listCategory.Find(o => o.Id == prod.IdCategory);
                listProductCategory.Add(obj);

            }

            return listProductCategory;
        }

        public ProductCategory GetbyID(object[] parameters)
        {
            return getIdProductCategory(parameters[0].ToString());
        }

        private ProductCategory getIdProductCategory(string id)
        {
            ProductCategory productCategory = null;
            Product product = null;
            Category category = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Product/" + id);

                string text = "";
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "GET";

                var responseEnd = (HttpWebResponse)request.GetResponse();
                if (responseEnd.StatusCode == HttpStatusCode.OK)
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text != null && text.Length > 1)
                {
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    product = json_serializer.Deserialize<Product>(text);
                }
                responseEnd.Close();
            }
            catch
            { }

            if(product != null)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Category/" + product.IdCategory);

                    string text = "";
                    request.ContentType = "application/json; charset=utf-8";
                    request.Method = "GET";

                    var responseEnd = (HttpWebResponse)request.GetResponse();
                    if (responseEnd.StatusCode == HttpStatusCode.OK)
                    {
                        using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                        {
                            text = sr.ReadToEnd();
                        }
                    }
                    if (text != null && text.Length > 1)
                    {
                        JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                        category = json_serializer.Deserialize<Category>(text);
                    }
                    responseEnd.Close();
                }
                catch
                { }

                if(category != null)
                {
                    productCategory = new ProductCategory();
                    productCategory.product = product;
                    productCategory.category = category;
                }

            }

            return productCategory;
        }

        public Int64 Insert(object[] parameters)
        {
            Product product = new Product() { Id = 0, DesProduct = parameters[0].ToString(), DesUrl = parameters[1].ToString(), IdCategory = Convert.ToInt64( parameters[2].ToString() )};
            Product obj = insertProduct(product);
            return obj.Id;
        }

        private Product insertProduct(Product product)
        {
            Product retProduct = new Product();
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                string strParameter = jsonserial.Serialize(product);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Product/");

                string text = "";
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(strParameter);
                    streamWriter.Close();
                }

                var responseEnd = (HttpWebResponse)request.GetResponse();
                if (responseEnd.StatusCode == HttpStatusCode.OK || responseEnd.StatusCode == HttpStatusCode.Created)
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text != null && text.Length > 1)
                {
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    retProduct = json_serializer.Deserialize<Product>(text);
                }
                responseEnd.Close();


            }
            catch
            { }
            return retProduct;
        }
        public Int64 Update(object[] parameters)
        {
            Product product = new Product() { Id = Convert.ToInt64(parameters[0].ToString()), DesProduct = parameters[1].ToString(), DesUrl = parameters[2].ToString(), IdCategory = Convert.ToInt64(parameters[3].ToString()) };
            Product obj = updateProduct(product);
            return obj.Id;
        }

        private Product updateProduct(Product product)
        {
            Product retProduct = new Product();
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                string strParameter = jsonserial.Serialize(product);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Product/" + product.Id);

                string text = "";
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "PUT";
                request.ContentLength = strParameter.Length;

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(strParameter);
                    streamWriter.Close();
                }

                var responseEnd = (HttpWebResponse)request.GetResponse();
                if (responseEnd.StatusCode == HttpStatusCode.OK || responseEnd.StatusCode == HttpStatusCode.Created)
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text != null && text.Length > 1)
                {
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    retProduct = json_serializer.Deserialize<Product>(text);
                }
                responseEnd.Close();

            }
            catch (Exception exc)
            {

            }
            return retProduct;
        }

        public Int64 Delete(object[] parameters)
        {
            return deleteProduct(Convert.ToInt64(parameters[0].ToString()));
        }

        private Int64 deleteProduct(Int64 id)
        {
            Int64 retorno = 0;
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Product/" + id);

                request.ContentType = "application/json; charset=utf-8";
                request.Method = "DELETE";
                var responseEnd = (HttpWebResponse)request.GetResponse();
                if (responseEnd.StatusCode == HttpStatusCode.OK || responseEnd.StatusCode == HttpStatusCode.Created || responseEnd.StatusCode == HttpStatusCode.NoContent)
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        retorno = 1;
                    }
                }
                responseEnd.Close();
            }
            catch
            {
            }

            return retorno;

        }
    }
}