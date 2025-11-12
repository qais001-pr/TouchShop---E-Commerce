namespace webapi.Models
{
    public class Admins
    {
        public int? adminid { get; set; }
        public string? full_name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? role { get; set; }
        public byte[]? profile_image { get; set; }

    }
}
