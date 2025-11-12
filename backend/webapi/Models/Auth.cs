namespace webapi.Models
{
    public class LoginRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }

    public class LoginResponse
    {
        public int? customer_id { get; set; }
        public string? full_name  { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phonenumber { get; set; }
        public byte[]? image { get; set; }
    }
}
