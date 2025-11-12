using Microsoft.Data.SqlClient;
using webapi.Models;
namespace webapi.Repository
{
    public class CategoryRepo
    {
        public readonly string connectionString;
        public CategoryRepo(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public async Task<bool> AddCategory(Category category)
        {
            if (await checkCategory(category.category_name))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO Categories (category_name,description) VALUES (@CategoryName, @Description)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryName", category.category_name);
                        command.Parameters.AddWithValue("@Description", category.description);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private async Task<bool> checkCategory(string name)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT COUNT(*) FROM Categories WHERE category_name=@cname", connection))
                {
                    command.Parameters.AddWithValue("@cname", name);
#pragma warning disable CS8605 // Unboxing a possibly null value.
                    int count = (int)await command.ExecuteScalarAsync();
#pragma warning restore CS8605 // Unboxing a possibly null value.
                    return count == 0;
                }
            }
        }

        public async Task<List<Category>> ShowAllCategory()
        {
            List<Category> customerList = new List<Category>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "Select * From Categories";
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Category c = new Category
                            {
                                category_id = reader.GetInt32(0),
                                category_name = reader.GetString(1),
                                description = reader.GetString(2),
                            };
                            customerList.Add(c);
                        }
                    }
                }
                await connection.CloseAsync();
            }
            return customerList;
        }



        public async Task updateCategory(Category category)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "update Categories set category_name=@CategoryName,description=@Description where category_id=@cid";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cid", category.category_id);
                    command.Parameters.AddWithValue("@CategoryName", category.category_name);
                    command.Parameters.AddWithValue("@Description", category.description);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCategory(Category category)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "Delete Categories where category_id=@cid";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cid", category.category_id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
