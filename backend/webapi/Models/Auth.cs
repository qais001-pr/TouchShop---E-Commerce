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
        public string? full_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phonenumber { get; set; }
        public byte[]? image { get; set; }
    }

    public class SignupRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? imageBytes { get; set; }
        public IFormFile? Image { get; set; }
    }
}
