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
        public async Task<IActionResult> LoginUser(Models.LoginRequest request)
        {
            var response = await _authRepo.LoginRequestandGetResponseofUSER(request);
            return Ok(new { status = 200, data = response });
        }
    }
}
