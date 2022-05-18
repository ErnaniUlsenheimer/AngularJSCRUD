using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Model
{
    public class Product
    {
        public static List<classes.Product> getAllProduct()
        {
            List<classes.Product> retorno = new List<classes.Product>();
            try
            {
                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    DataSet mDataSet = new DataSet();
                    MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT a.*, b.idcategory FROM tb_products a INNER JOIN tb_productscategories b ON a.idproduct = b.idproduct", mConn);

                    //preenche o dataset via adapter

                    mAdapter.Fill(mDataSet, "Product");

                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        classes.Product obj = new classes.Product();
                        obj.Id = Convert.ToInt64(mDataSet.Tables[0].Rows[i]["idproduct"].ToString());
                        obj.DesProduct= mDataSet.Tables[0].Rows[i]["desproduct"].ToString();
                        obj.DesUrl = mDataSet.Tables[0].Rows[i]["desurl"].ToString();
                        obj.IdCategory = Convert.ToInt64(mDataSet.Tables[0].Rows[i]["idcategory"].ToString());

                        retorno.Add(obj);
                    }

                }
                mConn.Close();
                mConn.Dispose();
            }
            catch
            { }
            return retorno;
        }

        public static Int64 insertProduct(classes.Product product)
        {
            Int64 retorno = 0;
            try
            {
                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "INSERT INTO tb_products (desproduct, desurl) VALUES (?desproduct, ?desurl)";
                    comm.Parameters.AddWithValue("?desproduct", product.DesProduct);
                    comm.Parameters.AddWithValue("?desurl", product.DesUrl);
                    comm.ExecuteNonQuery();
                    retorno = comm.LastInsertedId;
                    product.Id = retorno;
                }
                mConn.Close();
                mConn.Dispose();

                if(retorno >0)
                {
                    mConn = new MySqlConnection(classes.functions.conexaoDB);
                    mConn.Open();
                    if (mConn.State == ConnectionState.Open)
                    {
                        MySqlCommand comm = mConn.CreateCommand();
                        comm.CommandText = "INSERT INTO tb_productscategories (idcategory, idproduct) VALUES (?idcategory, ?idproduct)";
                        comm.Parameters.AddWithValue("?idcategory", product.IdCategory);
                        comm.Parameters.AddWithValue("?idproduct", product.Id);
                        comm.ExecuteNonQuery();
                       
                    }
                    mConn.Close();
                    mConn.Dispose();
                }
            }

            catch(Exception exc)
            {
                string msg = exc.Message;
                if(msg !=null)
                {
                    msg = "Erro";
                }
            }

            return retorno;
        }

        public static bool updateProduct(classes.Product product)
        {
            bool retorno = false;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "UPDATE tb_products SET desproduct ='" + product.DesProduct + "', desurl = '" + product.DesUrl + "' WHERE idproduct =" + product.Id;

                    comm.ExecuteNonQuery();
                    retorno = true;
                }
                mConn.Close();
                mConn.Dispose();

                if(retorno)
                {
                    mConn = new MySqlConnection(classes.functions.conexaoDB);
                    mConn.Open();
                    if (mConn.State == ConnectionState.Open)
                    {
                        MySqlCommand comm = mConn.CreateCommand();
                        comm.CommandText = "UPDATE tb_productscategories SET idcategory ='" + product.IdCategory + "' WHERE idproduct =" + product.Id;

                        comm.ExecuteNonQuery();
                        
                    }
                    mConn.Close();
                    mConn.Dispose();
                }
            }

            catch
            { }

            return retorno;
        }

        public static bool deleteProduct(classes.Product product)
        {
            bool retorno = false;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "DELETE FROM tb_productscategories WHERE idproduct =" + product.Id;
                   

                    comm.ExecuteNonQuery();
                    retorno = true;
                }
                mConn.Close();
                mConn.Dispose();
                if(retorno)
                {
                    mConn = new MySqlConnection(classes.functions.conexaoDB);
                    mConn.Open();
                    if (mConn.State == ConnectionState.Open)
                    {
                        MySqlCommand comm = mConn.CreateCommand();
                        comm.CommandText = "DELETE FROM tb_products WHERE idproduct =" + product.Id;

                        comm.ExecuteNonQuery();
                        retorno = true;
                    }
                    mConn.Close();
                    mConn.Dispose();

                }
            }
            catch (Exception exc)
            {
                string msg = exc.Message;
                if (msg != null)
                {
                    msg = "Erro";
                }
            }

            return retorno;
        }
    }
}
