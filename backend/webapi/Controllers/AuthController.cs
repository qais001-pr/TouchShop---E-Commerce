using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly AuthRepo? _authRepo;
        public AuthController(AuthRepo authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("LoginCustomer")]
        public async Task<IActionResult> LoginUser([FromBody] Models.LoginRequest request)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var response = await _authRepo.LoginRequestandGetResponseofUSER(request);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Console.WriteLine(response);
            if (response == null)
            {
                return BadRequest(new { status = 400, message = "Login Failed" });
            }
            return Ok(new { status = 200, data = response, message = "Login Successfull" });
        }
        [HttpPost("SignUpCustomer")]
        public async Task<IActionResult> SignupUser([FromForm] Models.SignupRequest request)
        {
            try
            {
                byte[]? imageBytes = null;
                if (request.Image != null && request.Image.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await request.Image.CopyToAsync(ms);
                        imageBytes = ms.ToArray();
                    }
                }
                request.imageBytes = imageBytes;

                var response = await _authRepo.SignupRequestFromCustomer(request);

                if (response == 500)
                {
                    return BadRequest(new { status = 500, message = "Email Already Exists" });
                }
                else if (response > 0)
                {
                    return Ok(new { status = 200, message = "Account Created Successfully" });
                }
                else
                {
                    return BadRequest(new { status = 400, message = "Something Went Wrong!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = 500, message = $"Internal server error" });
            }
        }

    }
}
