using Microsoft.Data.SqlClient;
using webapi.Models;

namespace webapi.Repository
{
    public class AdminRepo
    {
        public string connectionString { get; set; }
#pragma warning disable CS8618
        public AdminRepo(IConfiguration connection)
#pragma warning restore CS8618
        {
#pragma warning disable CS8601
            this.connectionString = connection.GetConnectionString("ConnectionString");
#pragma warning restore CS8601
        }
        public async Task<Boolean> createAdmin(Admins admins)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("Select Count(*) From Admins where email=@email", connection))
                {
                    command.Parameters.AddWithValue("@email", admins.email);
#pragma warning disable CS8605
                    int count = (int)await command.ExecuteScalarAsync();
#pragma warning restore CS8605
                    if (count == 0)
                    {
                        string query = "INSERT INTO Admins (full_name,email,password,role,profile_image) values(@fullname,@email,@password,@role,@profileimage)";
                        var commands = new SqlCommand(query, connection);
                        commands.Parameters.AddWithValue("@fullname", admins.full_name);
                        commands.Parameters.AddWithValue("@email", admins.email);
                        commands.Parameters.AddWithValue("@password", admins.password);
                        commands.Parameters.AddWithValue("@role", admins.role);
                        commands.Parameters.AddWithValue("@profileimage", admins.profile_image);
                        await command.ExecuteNonQueryAsync();
                        await connection.CloseAsync();
                        return true;
                    }
                    else
                    {
                        await connection.CloseAsync();
                        return false;
                    }
                }
            }
        }
        public async Task removeAdmin(int adminId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Admins WHERE admin_id=@adminId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@adminId", adminId);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }

        public async Task updateAdmin(Admins admin)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "Update Admins set full_name = @fullname,email=@emails,password=@password,role=@role,profile_image=@image WHERE admin_id=@adminId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@adminId", admin.adminid);
                command.Parameters.AddWithValue("@fullname", admin.full_name);
                command.Parameters.AddWithValue("@email", admin.email);
                command.Parameters.AddWithValue("@password", admin.password);
                command.Parameters.AddWithValue("@role", admin.role);
                command.Parameters.AddWithValue("@profileimage", admin.profile_image);
                await command.ExecuteNonQueryAsync();
                await connection.CloseAsync();
            }
        }
    }
}
