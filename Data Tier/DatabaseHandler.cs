using System;
using System.Data.SqlClient;
using System.Data;
namespace DataTier
{
    public class DatabaseHandler:Product
    {
        SqlConnection connection;//Connection object
        public DatabaseHandler()
        {
            try
            {
                //Connection string
                connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\arsla\source\repos\VPMidTask2\DataTier\MarketProductsDatabase.mdf;Integrated Security=True");
                connection.Open();
            }
            catch (Exception e) { }
        }
        public DataTable LoadData()
        {
            string loadQuery = "Select ProductID , ProductName ,DateOfEntrance ,ProductSold,ProductAvailable from Products";
            //To store output in the datatable
            DataTable products = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(loadQuery, connection);
            //Filling datatable with the data adapter
            dataAdapter.Fill(products);
            return products;
        }
        public string StoreData(DatabaseHandler product)
        {
            try
            {
                string storeQuery = "Insert into Products (ProductID , ProductName ,DateOfEntrance ,ProductSold,ProductAvailable) values" + " ('" + product.ProductId + "','" + product.Productame + "','" + product.EntryDate + "','" + product.ProductSold + "','" + product.ProductAvailable + "')";
                SqlCommand insert = new SqlCommand(storeQuery, connection);
                insert.ExecuteNonQuery();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public SqlDataReader SearchData(string value, int choice)
        {
            if (choice == 1)//If user searches through ID
            {
                string searchQuery = "Select *from Products where ProductID = '" + value + "'";
                SqlCommand search = new SqlCommand(searchQuery, connection);
                SqlDataReader reader = search.ExecuteReader();
                return reader;
            }
            else if (choice == 2)//If user searches through Name
            {
                string searchQuery = "Select *from Products where ProductName = '" + value + "'";
                SqlCommand search = new SqlCommand(searchQuery, connection);
                SqlDataReader reader = search.ExecuteReader();
                return reader;
            }
            else if(choice==3)//If user searches through Date of Entrance
            {
                string searchQuery = "Select *from Products where DateOfEntrance = '" + value + "'";
                SqlCommand search = new SqlCommand(searchQuery, connection);
                SqlDataReader reader = search.ExecuteReader();
                return reader;
            }
            else if (choice == 4)//If user searches through Sold status
            {
                string searchQuery = "Select *from Products where ProductSold = '" + value + "'";
                SqlCommand search = new SqlCommand(searchQuery, connection);
                SqlDataReader reader = search.ExecuteReader();
                return reader;
            }
            else //If user searches through Available status
            {
                string searchQuery = "Select *from Products where ProductAvailable = '" + value + "'";
                SqlCommand search = new SqlCommand(searchQuery, connection);
                SqlDataReader reader = search.ExecuteReader();
                return reader;
            }
        }
        public string UpdateData(string value, int choice, string identifier)
        {
            try
            {
                if (choice == 1)//If user wants to update ID
                {
                    string updateQuery = "Update Products set ProductID = '" + value + "'where ProductID = '" + identifier + "'";
                    SqlCommand update = new SqlCommand(updateQuery, connection);
                    update.ExecuteReader();
                }
                else if (choice == 2)// If user wants to update name
                {
                    string updateQuery = "Update Products set ProductName = '" + value + "'where ProductID = '" + identifier + "'";
                    SqlCommand update = new SqlCommand(updateQuery, connection);
                    update.ExecuteReader();
                }
                else if(choice==3)// If user wants to update date of entrance
                {
                    string updateQuery = "Update Products set DateOfEntrance = '" + value + "'where ProductID = '" + identifier + "'";
                    SqlCommand update = new SqlCommand(updateQuery, connection);
                    update.ExecuteReader();
                }
                else if(choice==4)// If user wants to update sold status
                {
                    string updateQuery = "Update Products set ProductSold = '" + value + "'where ProductID = '" + identifier + "'";
                    SqlCommand update = new SqlCommand(updateQuery, connection);
                    update.ExecuteReader();
                }
                else//If user wants to update available status
                {
                    string updateQuery = "Update Products set ProductAvailable = '" + value + "'where ProductID = '" + identifier + "'";
                    SqlCommand update = new SqlCommand(updateQuery, connection);
                    update.ExecuteReader();
                }
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public string DeleteData(string identifier)
        {
            try
            {
                string deleteQuery = "Delete from Products where ProductID = '" + identifier + "'";
                SqlCommand delete = new SqlCommand(deleteQuery, connection);
                delete.ExecuteReader();
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
