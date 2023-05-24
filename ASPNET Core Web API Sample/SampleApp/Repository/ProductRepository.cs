using SampleApp.Model;
using System.Data.SqlClient;

namespace SampleApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
           this._connectionString =  configuration.GetConnectionString("localSQL");
        }

        

        public async Task<List<Product>> FetchAll()
        {
            
            using(SqlConnection connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                    List<Product> products = new List<Product>();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "select Id, Name, Price, Description, Category from ProductsCatalog";
                        var reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            products.Add(new Product()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDouble(2),
                                Description = reader.GetString(3),
                                Catalog = reader.GetString(4),

                            });
                        }
                    }
                    return products;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
                
                
            }
        }

        
        

        public async Task<Product?> FetchById(int id)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                    
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "select Id, Name, Price, Description, Category from ProductsCatalog where Id=@id";
                        command.Parameters.AddWithValue("@id", id);
                        var reader = await command.ExecuteReaderAsync();
                        
                        if (reader.Read())
                        {
                            Product product = new Product();
                            product = new Product()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDouble(2),
                                Description = reader.GetString(3),
                                Catalog = reader.GetString(4),

                            };
                            return product;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


            }
        }

        public async Task<bool> Save(Product product)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "Insert into ProductsCatalog (Id, Name, Price, Description, Category) values(@id, @name, @price, @desc, @catalog)";

                        command.Parameters.AddWithValue("@id", product.Id);
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@desc", product.Description);
                        command.Parameters.AddWithValue("@catalog", product.Catalog);

                        var rowsAfftected = await command.ExecuteNonQueryAsync();
                        if (rowsAfftected == 1)
                        {
                            return true;
                        }

                    }
                    return false;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


            }
            
        }

        public async Task<bool> Delete(int productId)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "Delete from ProductsCatalog where id=@id";
                        command.Parameters.AddWithValue("@id", productId);
                        var rowsAfftected = await command.ExecuteNonQueryAsync();
                        if(rowsAfftected == 1)
                        {
                            return true;
                        }
                        
                    }
                    return false;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


            }
        }

        public async Task<bool> Update(Product product)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "Update ProductsCatalog set Name=@name, Price=@price, Description=@desc, Category=@category where id=@id";

                        command.Parameters.AddWithValue("@id", product.Id);
                        command.Parameters.AddWithValue("@name", product.Name);
                        command.Parameters.AddWithValue("@price", product.Price);
                        command.Parameters.AddWithValue("@desc", product.Description);
                        command.Parameters.AddWithValue("@category", product.Catalog);

                        var rowsAfftected = await command.ExecuteNonQueryAsync();
                        if (rowsAfftected == 1)
                        {
                            return true;
                        }

                    }
                    return false;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


            }
        }

        public async Task<bool> UpdatePrice(int productId, double price)
        {
            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "Update ProductsCatalog set Price=@price where id=@id";

                        command.Parameters.AddWithValue("@id", productId);
                        command.Parameters.AddWithValue("@price", price);
                       

                        var rowsAfftected = await command.ExecuteNonQueryAsync();
                        if (rowsAfftected == 1)
                        {
                            return true;
                        }

                    }
                    return false;
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


            }
        }
    }
}
