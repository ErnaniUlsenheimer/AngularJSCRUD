using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Model
{
    public class Category
    {
        public static List<classes.Category> getAllCategory()
        {
            List<classes.Category> retorno = new List<classes.Category>();
            try
            {
                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    DataSet mDataSet = new DataSet();
                    MySqlDataAdapter mAdapter = new MySqlDataAdapter("SELECT * FROM tb_categories", mConn);

                    //preenche o dataset via adapter

                    mAdapter.Fill(mDataSet, "Category");

                    for (int i = 0; i < mDataSet.Tables[0].Rows.Count; i++)
                    {
                        classes.Category obj = new classes.Category();
                        obj.Id = Convert.ToInt32(mDataSet.Tables[0].Rows[i]["idcategory"].ToString());
                        obj.DesCategory = mDataSet.Tables[0].Rows[i]["descategory"].ToString();
                        obj.CodeCategory = mDataSet.Tables[0].Rows[i]["codecategory"].ToString();

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

        public static Int64 insertCategory(classes.Category category)
        {
            Int64 retorno = 0;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "INSERT INTO tb_categories (descategory, codecategory) VALUES (?descategory, ?codecategory)";
                    comm.Parameters.AddWithValue("?descategory", category.DesCategory);
                    comm.Parameters.AddWithValue("?codecategory", category.CodeCategory);
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

        public static bool updateCategory(classes.Category category)
        {
            bool retorno = false;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "UPDATE tb_categories SET descategory ='" + category.DesCategory + "', codecategory = '" + category.CodeCategory + "' WHERE idcategory =" + category.Id;

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

        public static bool deleteCategory(classes.Category category)
        {
            bool retorno = false;
            try
            {

                MySqlConnection mConn = new MySqlConnection(classes.functions.conexaoDB);
                mConn.Open();
                if (mConn.State == ConnectionState.Open)
                {
                    MySqlCommand comm = mConn.CreateCommand();
                    comm.CommandText = "DELETE FROM tb_categories WHERE idcategory =" + category.Id;

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
