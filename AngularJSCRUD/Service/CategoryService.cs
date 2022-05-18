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
    public partial class CategoryService
    {
        private List<Category> CustRepository;

        public CategoryService()
        {
            this.CustRepository = new List<Category>();
        }

        public IEnumerable<Category> GetAll(object[] parameters)
        {
            List<Category> lCategory = new List<Category>();
            try
            {
                lCategory = getAllCategory();
            }
            catch
            { }
            return lCategory.OrderBy(o=>o.Id).ToList().ToArray();

        }

        private List<Category> getAllCategory()
        {
            List<Category> listCategory = new List<Category>();
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
            return listCategory;
        }

        public Category GetbyID(object[] parameters)
        {
            return getIdCategory(parameters[0].ToString());
        }

        private Category getIdCategory(string id)
        {
            Category listCategory = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Category/" + id);

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
                    listCategory = json_serializer.Deserialize<Category>(text);
                }
                responseEnd.Close();


            }
            catch
            { }
            return listCategory;
        }

        public Int64 Insert(object[] parameters)
        {
            Category category = new Category() { Id = 0, DesCategory = parameters[0].ToString(), CodeCategory = parameters[1].ToString() };
            Category obj = insertCategory(category);
            return obj.Id;
        }

        private Category insertCategory(Category category)
        {
            Category retCategory = new Category();
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                string strParameter = jsonserial.Serialize(category);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Category/");

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
                    retCategory = json_serializer.Deserialize<Category>(text);
                }
                responseEnd.Close();


            }
            catch
            { }
            return retCategory;
        }
        public Int64 Update(object[] parameters)
        {
            Category category = new Category() { Id = Convert.ToInt64(parameters[0].ToString()), DesCategory = parameters[1].ToString(), CodeCategory = parameters[2].ToString() };
            Category obj = updateCategory(category);
            return obj.Id;
        }

        private Category updateCategory(Category category)
        {
            Category retCategory = new Category();
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                string strParameter = jsonserial.Serialize(category);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Category/" + category.Id);

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
                    retCategory = json_serializer.Deserialize<Category>(text);
                }
                responseEnd.Close();

            }
            catch (Exception exc)
            {

            }
            return retCategory;
        }

        public Int64 Delete(object[] parameters)
        {
            return deleteCategory(Convert.ToInt64(parameters[0].ToString()));
        }

        private Int64 deleteCategory(Int64 id)
        {
            Int64 retorno = 0;
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Category/" + id);

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