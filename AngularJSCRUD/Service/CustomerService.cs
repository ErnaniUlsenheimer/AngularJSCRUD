
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
    public partial class CustomerService
    {
        private List<Customer> CustRepository;

        public CustomerService()
        {
            this.CustRepository = new List<Customer>();
        }

        public IEnumerable<Customer> GetAll(object[] parameters)
        {
            List<Customer> lCustomer = new List<Customer>();
            try
            {
                lCustomer = getAllCustomer();
            }
            catch
            { }
            return lCustomer.ToArray();

        }

        private List<Customer> getAllCustomer()
        {
            List<Customer> listCustomer = new List<Customer>();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Customer");

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
                    listCustomer = json_serializer.Deserialize<List<Customer>>(text);
                }
                responseEnd.Close();

                
            }
            catch
            { }
            return listCustomer;
        }

        public Customer GetbyID(object[] parameters)
        {
            return getIdCustomer(parameters[0].ToString());
        }

        private Customer getIdCustomer(string id)
        {
            Customer listCustomer = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Customer/" + id);

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
                    listCustomer = json_serializer.Deserialize<Customer>(text);
                }
                responseEnd.Close();


            }
            catch
            { }
            return listCustomer;
        }

        public Int64 Insert(object[] parameters)
        {
            Customer customer = new Customer() { Id = 0, CustomerName = parameters[0].ToString(), CustomerCode = parameters[1].ToString() };
            Customer obj = insertCustomer(customer);
            return obj.Id;           
        }

        private Customer insertCustomer(Customer customer)
        {
            Customer retCustomer = new Customer();
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                string strParameter = jsonserial.Serialize(customer);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Customer/");

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
                    retCustomer = json_serializer.Deserialize<Customer>(text);
                }
                responseEnd.Close();


            }
            catch
            { }
            return retCustomer;
        }
        public Int64 Update(object[] parameters)
        {
            Customer customer = new Customer() { Id = Convert.ToInt64(parameters[0].ToString()), CustomerName = parameters[1].ToString(), CustomerCode = parameters[2].ToString() };
            Customer obj = updateCustomer(customer);
            return obj.Id;
        }

        private Customer updateCustomer(Customer customer)
        {
            Customer retCustomer = new Customer();
            try
            { 
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();
                string strParameter = jsonserial.Serialize(customer);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Customer/" + customer.Id);

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
                if (responseEnd.StatusCode == HttpStatusCode.OK || responseEnd.StatusCode == HttpStatusCode.Created )
                {
                    using (var sr = new StreamReader(responseEnd.GetResponseStream()))
                    {
                        text = sr.ReadToEnd();
                    }
                }
                if (text != null && text.Length > 1)
                {
                    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                    retCustomer = json_serializer.Deserialize<Customer>(text);
                }
                responseEnd.Close();

            }
            catch(Exception exc)
            {
               
            }
            return retCustomer;
        }

        public Int64 Delete(object[] parameters)
        {            
            return deleteCustomer(Convert.ToInt64(parameters[0].ToString()));
        }

        private Int64 deleteCustomer(Int64 id)
        {
            Int64 retorno = 0;
            try
            {
                JavaScriptSerializer jsonserial = new JavaScriptSerializer();                
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:10010/api/Customer/" + id);
                
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