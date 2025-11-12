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

        public async Task<LoginResponse> LoginRequestandGetResponseofUSER(Models.LoginRequest request)
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
                        if (await reader.ReadAsync())
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

            return response;
        }

        public async Task<int> SignupRequestFromCustomer(SignupRequest request)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Check if email exists
                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE email = @Email", connection))
                {
                    checkCmd.Parameters.AddWithValue("@Email", request.Email);
                    int exists = (int)await checkCmd.ExecuteScalarAsync();
                    if (exists > 0)
                        return 500;
                }

                // Insert user data
                using (var cmd = new SqlCommand(@"
            INSERT INTO Customers (full_name, email, password, phone, profile_image)
            OUTPUT INSERTED.customer_id
            VALUES (@FullName, @Email, @Password, @Phone, @Image)", connection))
                {
                    cmd.Parameters.AddWithValue("@FullName", request.FullName);
                    cmd.Parameters.AddWithValue("@Email", request.Email);
                    cmd.Parameters.AddWithValue("@Password", request.Password);
                    cmd.Parameters.AddWithValue("@Phone", request.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Image", request.imageBytes);

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result);
                }
            }
        }
    }
}
