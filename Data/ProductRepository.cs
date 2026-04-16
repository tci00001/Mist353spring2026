using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using SportsStore_Spr2026.Models;
using System.Data;

namespace SportsStore_Spr2026.Data
{
    public class ProductRepository : IProductRepository
    {
        //so this class is going to impliment the contract of the IProductRepository interface and then we will add the code to talk to the database and get the data we need.
        //to talk to the database we need a connection string

        private readonly string connectionString;
        //we need to get it to grab it from appsettings
        //you can do this by using the constructor of the class and then you can use the configuration manager to get the connection string from appsettings
        //the configuration file refers to the appsettings.jsob

        public ProductRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public List<Product> GetProductList()
        {

            List<Product> products = new List<Product>();
            //we start talking to the database, we need to
            //1. Create a connection to the database using the connection string
            //2. Create a command to execute the SQL query
            //3. Open the connection
            //4. Run the command and get the data
            //5. Close the connection
            using (var connection = new SqlConnection(connectionString))
            {
                //using this connection throughout the body. 
                //command object
                //using defines a scope at the end of which an object will be disposed
                //to pass in values to the
                using (var command = new SqlCommand("spGetAllProducts", connection))
                {
                    //defines the command type which will be ran
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();


                    //run the command after opening the connection
                    //sql reader object to read the data that is returned from the database
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Price = reader.GetDecimal(3),
                                thumbnail = reader.GetString(4),
                                image = reader.GetString(5),
                                PromoFront = reader.GetBoolean(6),
                                PromoDept = reader.GetBoolean(7)
                            });


                        }

                    }


                }

                //a using statement will close the connection once its out of scope

            }
            //
            return products;
        }
        public Product GetProductById(int productid)
        {
            Product retrievedProduct = null;

            using (var connection = new SqlConnection(connectionString))
            {
                var sqlcommand = new SqlCommand("spGetProductById", connection);
                sqlcommand.CommandType = System.Data.CommandType.StoredProcedure;


                sqlcommand.Parameters.Add(new SqlParameter("@ProductId", productid));

                connection.Open();
                //select statement means you will use execute reader
                using (var reader = sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //so this reads a row then uses C# to create a new product object and populate it with the data from the database and then return that product object
                        retrievedProduct = new Product
                        {
                            ProductId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            thumbnail = reader.GetString(4),
                            image = reader.GetString(5),
                            PromoFront = reader.GetBoolean(6),
                            PromoDept = reader.GetBoolean(7)
                        };


                    }







                }
            }



            return retrievedProduct;
        }





        public void CreateProduct(Product product)
        {


            var paramaters = new SqlParameter[]
            {
                new SqlParameter("@name", SqlDbType.NVarChar ) { Value = product.Name },
                new SqlParameter("@description",SqlDbType.NVarChar) { Value = product.Description },
                new SqlParameter("@price", SqlDbType.Money) { Value = product.Price },
                new SqlParameter("@thumbnail", SqlDbType.NVarChar) { Value = product.thumbnail },
                new SqlParameter("@image", SqlDbType.NVarChar) { Value = product.image },
                new SqlParameter("@PromoFront", SqlDbType.Bit) { Value = product.PromoFront },
                new SqlParameter("@PromoDept", SqlDbType.Bit) { Value = product.PromoDept },
                new SqlParameter("@newProdID", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };



            //now that we have the paramaters done for sql we can create the connection to the database and then create a command to execute the stored procedure that will insert the product into the database and then we will pass in the paramaters to that stored procedure and then execute it.
            //this method will take a product object and insert it into the database
            using (var connection = new SqlConnection(connectionString))
            {

                connection.Open();


                using (var command = new SqlCommand("SpInsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(paramaters);
                    command.ExecuteScalar();
                    //after executing the stored procedure we can get the value of the output parameter which will be the new product id that was generated by the database and then we can set that value to the product object that was passed in so that we can return it to the caller if needed.


                }



            }
        }


            public bool UpdateProduct(Product product)
            {
                     using (var connection = new SqlConnection(connectionString))
                     {
                        using(var command = new SqlCommand("spUpdateProduct",connection))
                         {
                            command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@prodid", product.ProductId);
                        command.Parameters.AddWithValue("@description", product.Description);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@thumbnail", product.thumbnail);
                        command.Parameters.AddWithValue("@image", product.image);
                        command.Parameters.AddWithValue("@promofront", product.PromoFront);
                        command.Parameters.AddWithValue("@promodept", product.PromoDept);
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                             }
                     }
            }
    










    }
 }
        

        

        //method signature is a void 

    

