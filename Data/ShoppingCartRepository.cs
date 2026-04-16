using Microsoft.Data.SqlClient;
using SportsStore_Spr2026.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SportsStore_Spr2026.Data
{
    public class ShoppingCartRepository:IShoppingCartRepository
    {
       
        private readonly string _connection;

        
        
        public ShoppingCartRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("DefaultConnection");
            //saving the connection string to the _connection variable
        }

        public void AddProductToCart(string cartID, int ProductId)
        {
            using (var connection = new SqlConnection(_connection))// tells program what connection to use 
            {
                using(var command = new SqlCommand("spShoppingCartAddItem",connection))// tells what command to use in the database 
                {
                    
                    command.CommandType= System.Data.CommandType.StoredProcedure;// tells that the command being used is a stored procedure

                    //gives the database the parameters the stored procedure needs to run
                    command.Parameters.AddWithValue("@cartID", cartID);
                    command.Parameters.AddWithValue("@prodID", ProductId);
                    command.Parameters.AddWithValue("@attributes", "none");
                    //open the connection
                    connection.Open();
                    //runs the command returns the amount of rows affected
                    command.ExecuteNonQuery();

                }
                

            }
        }


        public List<ShoppingCart> LoadCartItems(string cartID, out decimal cartTotal)
        {
            List<ShoppingCart> CartItems = new List<ShoppingCart>();

            using (var connection = new SqlConnection(_connection))
            {
                connection.Open();
                using (var command = new SqlCommand("spShoppingCartGetItems",connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cartID", cartID);

                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CartItems.Add(

                                new ShoppingCart
                                {
                                    CartID = reader.GetString(0),
                                    ProductID = reader.GetInt32(1),
                                    Name = reader.GetString(2),
                                    Price = reader.GetDecimal(3),
                                    Quantity = reader.GetInt32(4),
                                    Subtotal = reader.GetDecimal(5)
                                } );

                        }
                    }
                }

                //fetch cart total
                using (var command = new SqlCommand("spShoppingCartGetTotalAmount", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cartID", cartID);

                    object result = command.ExecuteScalar();

                    cartTotal = (decimal)result;

                }
            }
            return CartItems;
        }

        public void UpdateCartItem(string cartID, int ProductId, int quantity)
        {
            using(var connection = new SqlConnection(_connection))
            {

                using (var command = new SqlCommand("spShoppingCartUpdateItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@cartID", cartID);
                    command.Parameters.AddWithValue("@prodID", ProductId);
                    command.Parameters.AddWithValue("@qty", quantity);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
        }



    }
}
