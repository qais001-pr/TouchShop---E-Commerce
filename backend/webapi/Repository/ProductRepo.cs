using Microsoft.Data.SqlClient;
using webapi.Models;

namespace webapi.Repository
{
    public class ProductRepo
    {

        public readonly string connectionString;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public ProductRepo(IConfiguration configuration)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            connectionString = configuration.GetConnectionString("ConnectionString");
#pragma warning restore CS8601 // Possible null reference assignment.
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
        public async Task<List<ProductDetails>> getProductwithdetails()
        {
            var products = new List<ProductDetails>();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                P.product_id, P.product_name, P.stock_quantity, P.description, 
                P.category_id, P.price, 
                PI.image_id, PI.image_data, 
                R.rating, 
                C.category_name, C.description AS category_description
            FROM PRODUCTS P
            LEFT JOIN ProductImages PI ON P.product_id = PI.product_id
            LEFT JOIN Reviews R ON P.product_id = R.product_id
            LEFT JOIN Categories C ON P.category_id = C.category_id;
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id"));

                            // Check if this product already exists in the list
                            var product = products.FirstOrDefault(p => p.productid == productId);
                            if (product == null)
                            {
                                product = new ProductDetails
                                {
                                    productid = productId,
                                    product_name = reader.GetString(reader.GetOrdinal("product_name")),
                                    stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    description = reader.GetString(reader.GetOrdinal("description")),
                                    categoryid = reader.GetInt32(reader.GetOrdinal("category_id")),
                                    price = (int?)reader.GetDecimal(reader.GetOrdinal("price")),
                                    category_name = reader.GetString(reader.GetOrdinal("category_name")),
                                    category_description = reader.GetString(reader.GetOrdinal("category_description")),
                                    Images = new List<ProductImage>(),
                                    Reviews = new List<ProductReview>()
                                };
                                products.Add(product);
                            }

                            // Add image if available
                            if (!reader.IsDBNull(reader.GetOrdinal("image_id")))
                            {
                                var imageId = reader.GetInt32(reader.GetOrdinal("image_id"));
                                if (!product.Images.Any(i => i.image_id == imageId))
                                {
                                    product.Images.Add(new ProductImage
                                    {
                                        image_id = imageId,
                                        image_data = reader["image_data"] as byte[]
                                    });
                                }
                            }

                            // Add review if available
                            if (!reader.IsDBNull(reader.GetOrdinal("rating")))
                            {
                                var rating = reader.GetInt32(reader.GetOrdinal("rating"));
                                product.Reviews.Add(new ProductReview { rating = rating });
                            }
                        }
                    }
                }
            }

            return products;
        }


        public async Task<List<ProductDetails>> getProductwithdetailsByid(int id)
        {
            var products = new List<ProductDetails>();

            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                P.product_id, P.product_name, P.stock_quantity, P.description,P.price,P.category_id,
                PI.image_id, PI.image_data,
                R.rating,R.review_id,R.comment,R.customer_id,
                C.category_name, C.description AS category_description,
				CC.full_name,CC.profile_image,CC.email
                FROM PRODUCTS p
                LEFT JOIN ProductImages PI ON P.product_id = PI.product_id
                LEFT JOIN Reviews R ON P.product_id = R.product_id
                LEFT JOIN Categories C ON P.category_id = C.category_id
			    LEFT JOIN Customers CC ON R.customer_id=CC.customer_id
		   WHERE P.product_id = @ProductId;
        ";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", id);
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int productId = reader.GetInt32(reader.GetOrdinal("product_id"));

                            // Check if this product already exists in the list
                            var product = products.FirstOrDefault(p => p.productid == productId);
                            if (product == null)
                            {
                                product = new ProductDetails
                                {
                                    productid = productId,
                                    product_name = reader.GetString(reader.GetOrdinal("product_name")),
                                    stockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    description = reader.GetString(reader.GetOrdinal("description")),
                                    categoryid = reader.GetInt32(reader.GetOrdinal("category_id")),
                                    price = (int?)reader.GetDecimal(reader.GetOrdinal("price")),
                                    category_name = reader.GetString(reader.GetOrdinal("category_name")),
                                    category_description = reader.GetString(reader.GetOrdinal("category_description")),
                                    Images = new List<ProductImage>(),
                                    Reviews = new List<ProductReview>()
                                };
                                products.Add(product);
                            }

                            // Add image if available
                            if (!reader.IsDBNull(reader.GetOrdinal("image_id")))
                            {
                                var imageId = reader.GetInt32(reader.GetOrdinal("image_id"));
                                if (!product.Images.Any(i => i.image_id == imageId))
                                {
                                    product.Images.Add(new ProductImage
                                    {
                                        image_id = imageId,
                                        image_data = reader["image_data"] as byte[]
                                    });
                                }
                            }

                            // Add review if available
                            if (!reader.IsDBNull(reader.GetOrdinal("rating")))
                            {
                                var rating = reader.GetInt32(reader.GetOrdinal("rating"));
                                var fullName = reader.GetString(reader.GetOrdinal("full_name"));
                                var profileImage = reader["profile_image"] as byte[];
                                var email = reader.GetString(reader.GetOrdinal("email"));
                                product.Reviews.Add(new ProductReview
                                {
                                    rating = rating,
                                    full_name = fullName,
                                    profile_image = profileImage,
                                    email = email
                                });
                            }
                        }
                    }
                }
            }

            return products;
        }
    }
}
