using Microsoft.AspNetCore.Mvc;
using webapi.Repository;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly AdminRepo adminRepo;
        public AdminController(AdminRepo adminRepo)
        {
            this.adminRepo = adminRepo;
        }
        [HttpPost("createAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] Admins admins)
        {
            if (!await adminRepo.createAdmin(admins))
            {
                return BadRequest(new { status = 500 , message = "Email Already Exists"});
            }
            return Ok(new { status = 500, message = "Email Already Exists" });
        }


        [HttpPut("updateAdmin")]
        public async Task<IActionResult> updateAdmin([FromBody] Admins admins)
        {
            await adminRepo.updateAdmin(admins);
            return Ok(new { status = 200, message = "Updated Successfully" });
        }

        [HttpDelete("removeAdmin/{id}")]
        public async Task<IActionResult> removeAdmin([FromBody] int id)
        {
            await adminRepo.removeAdmin(id);
            return Ok(new { status = 200, message = "Deleted Successfully" });
        }
    }
}
