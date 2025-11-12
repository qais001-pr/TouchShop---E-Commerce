using Microsoft.Data.SqlClient;
using webapi.Models;

namespace webapi.Repository
{
    public class CustomerRepo
    {
        public readonly string connectionString;
        public CustomerRepo(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
        }
        public async Task AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "INSERT INTO Customers (full_name,Email,Password,Phone,profile_image) VALUES (@FullName, @Email,@Password,@Phone,@profile_image)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", customer.FullName);
                    command.Parameters.AddWithValue("@Email", customer.email);
                    command.Parameters.AddWithValue("@Password", customer.password);
                    command.Parameters.AddWithValue("@Phone", customer.phonenumber);
                    command.Parameters.AddWithValue("@profile_image", customer.image);
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "SELECT Customer_ID, full_name, Email, Password, Phone, profile_image FROM Customers";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Customer customer = new Customer
                            {
                                CustomerID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                email = reader.GetString(2),
                                password = reader.GetString(3),
                                phonenumber = reader.GetString(4),
                                image = (byte[])reader["profile_image"]
                            };
                            customers.Add(customer);
                        }
                    }
                }
                await connection.CloseAsync();
            }
            return customers;
        }
        public async Task updateCustomers(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "update Customers set full_name = @fullname,email=@email,password=@password,phone=@phone,profile_image=@image where Customer_ID = @Cid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cid", customer.CustomerID);
                    command.Parameters.AddWithValue("@fullname", customer.FullName);
                    command.Parameters.AddWithValue("@email", customer.email);
                    command.Parameters.AddWithValue("@password", customer.password);
                    command.Parameters.AddWithValue("@phone", customer.phonenumber);
                    command.Parameters.AddWithValue("@image", customer.image);
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
        public async Task deleteCustomers(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string query = "Delete Customers where Customer_ID = @Cid";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cid", customer.CustomerID);
                    await command.ExecuteNonQueryAsync();
                }
                await connection.CloseAsync();
            }
        }
    }
}
