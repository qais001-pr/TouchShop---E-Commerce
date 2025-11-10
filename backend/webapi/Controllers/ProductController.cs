using webapi.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.Repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ProductRepo productRepo;

        public ProductController(ProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        [HttpPost]
        [Route("Add Product")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product != null || !string.IsNullOrEmpty(product?.product_name)
                || product?.productid != null
                || product?.created_by_admins != null
                || product?.price != null
                || product?.stock_quantity != null
                || product?.description != null
                || product?.category_id != null)
            {

                return Ok(new { status = 200, message = "Data Saved Successfully" });
            }
            return BadRequest("Invalid product data.");
        }
    }
}
