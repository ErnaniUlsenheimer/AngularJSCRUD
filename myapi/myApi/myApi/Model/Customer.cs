using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace myApi.Model
{
    public class Customer
    {

        public static List<classes.Customer> getAllCustomer()
        {
            List<classes.Customer> retorno = new List<classes.Customer>();
            try
            {
                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    DataSet mDataSet = new DataSet();
                    MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM tb_customer", mConn);

                    //preenche o dataset via adapter

                    mAdapter.Fill(mDataSet, "Customer");

                    for(int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        classes.Customer obj = new classes.Customer();
                        obj.Id = Convert.ToInt32(mDataSet.Tables[0].Rows[i]["idcustomer"].ToString());
                        obj.CustomerName = mDataSet.Tables[0].Rows[i]["descustomer"].ToString();
                        obj.CustomerCode= mDataSet.Tables[0].Rows[i]["codecustomer"].ToString();

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

        public static Int64 insertCustomer(classes.Customer cutomer)
        {
            Int64 retorno = 0;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "INSERT INTO tb_customer (descustomer, codecustomer) VALUES (?descustomer, ?codecustomer)";
                    comm.Parameters.AddWithValue("?descustomer", cutomer.CustomerName);
                    comm.Parameters.AddWithValue("?codecustomer", cutomer.CustomerCode);
                    comm.ExecuteNonQuery();
                    retorno = comm.LastInsertedId;
                }
                mConn.Close();
                mConn.Dispose();
            }

            catch
            { }

            return retorno;
        }

        public static bool updateCustomer(classes.Customer cutomer)
        {
            bool retorno = false;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "UPDATE tb_customer SET descustomer ='" + cutomer.CustomerName +"', codecustomer = '" + cutomer.CustomerCode +"' WHERE idcustomer =" + cutomer.Id;
                   
                    comm.ExecuteNonQuery();
                    retorno = true;
                }
                mConn.Close();
                mConn.Dispose();
            }

            catch
            { }

            return retorno;
        }

        public static bool deleteCustomer(classes.Customer cutomer)
        {
            bool retorno = false;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "DELETE FROM tb_customer WHERE idcustomer =" + cutomer.Id;

                    comm.ExecuteNonQuery();
                    retorno = true;
                }
                mConn.Close();
                mConn.Dispose();
            }

            catch
            { }

            return retorno;
        }



    }
}
