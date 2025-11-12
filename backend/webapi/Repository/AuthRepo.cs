using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Data.SqlClient;
using webapi.Models;
namespace webapi.Repository
{
    public class AuthRepo
    {
        public string connectionString { get; set; }

        public AuthRepo(IConfiguration connection)
        {
            this.connectionString = connection.GetConnectionString("ConnectionString")!;
        }

        public async Task<LoginResponse?> LoginRequestandGetResponseofUSER(Models.LoginRequest request)
        {
            LoginResponse? response = null;

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT customer_id, full_name, email,phone,password, profile_image FROM Customers WHERE email=@Email AND password=@Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", request.email);
                    command.Parameters.AddWithValue("@Password", request.password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // ✅ Moves to the first record
                        {
                            response = new LoginResponse
                            {
                                customer_id = reader.GetInt32(0),
                                full_name = reader.GetString(1),
                                email = reader.GetString(2),
                                phonenumber = reader.GetString(3),
                                password = reader.GetString(4),
                                image = (byte[])reader[5]
                            };
                        }
                    }
                }
            }

            return response; // null means invalid login
        }

    }
}
