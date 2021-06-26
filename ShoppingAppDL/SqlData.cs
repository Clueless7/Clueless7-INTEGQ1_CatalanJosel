using ShoppingAppCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ShoppingAppDL
{
    public class SqlData
    {
        static string connectionString = "Server=tcp:168.63.255.42,1433; Database=ShoppingApp;User Id=sa;Password=integdatabase1!;";
        static SqlConnection sqlConnection = new SqlConnection(connectionString);

        static public List<Product> GetAllProductsData()
        {
            var selectStatement = "SELECT ProductId, Name, Quantity FROM Products";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var productList = new List<Product>();

            while (reader.Read())
            {
                productList.Add(new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    Name = reader["Name"].ToString()
                });
            }

            sqlConnection.Close();
            return productList;
        }

        static public Product GetProductData(int productId)
        {
            var selectStatement = "SELECT * FROM Products WHERE ProductId = @ProductId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@ProductId", productId);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();

            var product = new Product();

            if (reader.Read())
            {
                product.ProductId = Convert.ToInt32(reader["ProductId"]);
                product.Name = reader["Name"].ToString();
                product.Quantity = Convert.ToInt32(reader["Quantity"]);
                product.Price = Convert.ToDouble(reader["Price"]);
                product.Info = reader["Info"].ToString();
            }

            sqlConnection.Close();
            return product;
        }

        static public int GetProductQuantity(int productId)
        {
            var selectStatement = "SELECT Quantity FROM Products WHERE ProductId = @ProductId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@ProductId", productId);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            int quantityRemaining = reader.Read() ? Convert.ToInt32(reader["Quantity"]) : 0;
            sqlConnection.Close();
            return quantityRemaining;
        }

        static public int GetProductPrice(int productId)
        {
            var selectStatement = "SELECT Price FROM Products WHERE ProductId = @ProductId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@ProductId", productId);
            sqlConnection.Open();
            SqlDataReader reader = selectCommand.ExecuteReader();
            int productPrice = reader.Read() ? Convert.ToInt32(reader["Price"]) : 0;
            sqlConnection.Close();
            return productPrice;
        }

        static public int ReduceProductQuantity(int quantityBuying, int productId)
        {
            int quantityRemaining = GetProductQuantity(productId);
            int quantityDifference = quantityRemaining - quantityBuying;
            if (quantityBuying > quantityRemaining)
            {
                return -2; //Means having greater buying quantity than remaining stocks
            } else if (quantityRemaining == 0)
            {
                return -3; //Means there is no remaining stocks
            } else if (quantityBuying < 0)
            {
                return -4; //Means the user entered a negative value
            }
            var updateStatement = $"UPDATE Products SET Quantity = @Quantity WHERE ProductId = @ProductId";
            SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);
            updateCommand.Parameters.AddWithValue("@Quantity", quantityDifference);
            updateCommand.Parameters.AddWithValue("@ProductId", productId);
            sqlConnection.Open();
            var result = updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return result;
        }
    }
}
