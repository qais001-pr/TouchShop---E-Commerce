namespace webapi.Models
{
    public class Product
    {
        public int? productid { get; set; }
        public int? category_id { get; set; }
        public string? product_name { get; set; }
        public string? description { get; set; }
        public int? price { get; set; }
        public int? stock_quantity { get; set; }
        public int? created_by_admins { get; set; }
    }

    public class ProductDetails
    {
        public int? productid { get; set; }
        public string? product_name { get; set; }
        public int? stockQuantity { get; set; }
        public string? description { get; set; }
        public int? categoryid { get; set; }
        public int? price { get; set; }
        public string? category_name { get; set; }
        public string? category_description { get; set; }
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public List<ProductReview> Reviews { get; set; } = new List<ProductReview>();

    }
    public class ProductImage
    {
        public int image_id { get; set; }
        public byte[]? image_data { get; set; }
    }

    public class ProductReview
    {
        public int rating { get; set; }
        public string? full_name { get; set; }
        public byte[] profile_image { get; set; }
        public string? email { get; set; }
    }
}
