using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using BasicCrudOprations.Models;
using System.Data.SqlClient;
using System.Data;

namespace BasicCrudOprations.DAL
{
    public class Category_DAL
    {
        string conString  = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        //Get All Category 

        public List<CategoryModel> GetAllCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllCategories";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    categories.Add(new CategoryModel
                    {
                        CategoryID = Convert.ToInt32(dr["CategoryID"]),
                        CategoryName = dr["CategoryName"].ToString(),
                    });
                }
            }
                return categories;
        }

        //Insert Categories

         public bool InsertCat(CategoryModel category)
         {
             int id = 0;

             using (SqlConnection connection = new SqlConnection(conString))
             {
                 SqlCommand cmd = new SqlCommand("InsertCategories", connection);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

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
        public List<CategoryModel> GetCategoryByID(int CategoryID)
        {
            List<CategoryModel> categories = new List<CategoryModel>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCategoryByID";
                cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    categories.Add(new CategoryModel
                    {
                        CategoryID = Convert.ToInt32(dr["CategoryID"]),
                        CategoryName = dr["CategoryName"].ToString(),
                    });
                }
            }
            return categories;
        }


        //Update Categories

        public bool UpdateCategories(CategoryModel category)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {

                    SqlCommand cmd = new SqlCommand("ModifyCategories", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CategoryID", category.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();
               
            }

            return rowsAffected > 0;
        }


        public string DeleteCategory(int categoryID) 
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("DeleteCategories",con);

                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
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