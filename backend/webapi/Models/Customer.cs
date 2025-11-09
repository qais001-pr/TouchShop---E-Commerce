namespace webapi.Models
{
    public class Customer
    {
        public int? CustomerID { get; set; }
        public string? FullName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phonenumber { get; set; }
        public byte[]? image { get; set; }
    }
}
