using System.Data.SqlClient;
using webapi.Models;

namespace webapi.Repository
{
    public class ProductRepo
    {

        public readonly string connectionString;

        public ProductRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task AddProduct(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Products (product_name,description,price,stock_quantity,created_by_admins) VALUES (@ProductName, @Description,@Price,@StockQuantity,@CreatedByAdmins)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductName", product.product_name);
                    command.Parameters.AddWithValue("@Description", product.description);
                    command.Parameters.AddWithValue("@Price", product.price);
                    command.Parameters.AddWithValue("@StockQuantity", product.stock_quantity);
                    command.Parameters.AddWithValue("@CreatedByAdmins", product.created_by_admins);
                    await command.ExecuteNonQueryAsync();
                }
            }

        }
        public async Task DeleteProduct(Product product)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "Delete Products where Product_id=@productid";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Productid", product.productid);
                    await command.ExecuteNonQueryAsync();
                }
            }

        }
    }
}
