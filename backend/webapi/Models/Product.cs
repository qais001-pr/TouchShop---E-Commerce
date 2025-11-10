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
}
