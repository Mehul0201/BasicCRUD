using BasicCrudOprations.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BasicCrudOprations.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> categories = new List<ProductModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllProducts";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    categories.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        CategoryID = Convert.ToInt32(dr["CategoryID"]),
                    });
                }
            }
            return categories;
        }

        //Insert Categories

        public bool InsertProducts(ProductModel product)
        {
            int id = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("InsertProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);

                connection.Open();
                id = cmd.ExecuteNonQuery();
                connection.Close();

            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Get Products by product ID
        public List<ProductModel> GetProductByID(int ProductID)
        {
            List<ProductModel> products = new List<ProductModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetProductByID";
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    products.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        //CategoryID = Convert.ToInt32(dr["CategoryID"]),
                    });
                }
            }
            return products;
        }


        //Update Categories

        public bool UpdateCategories(ProductModel product)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand("ModifyProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);

                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();

            }

            return rowsAffected > 0;
        }


        public string DeleteProducts(int ProductID)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.Add("@outputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@outputMessage"].Value.ToString();
                con.Close();

            }

            return result;
        }



    }
}
