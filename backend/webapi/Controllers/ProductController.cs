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

        [HttpGet]
        [Route("GetProductDetails")]
        public async Task<IActionResult> GetProductDetails()
        {
            var productDetails = await productRepo.getProductwithdetails();
            if (productDetails != null)
            {
                return Ok(new { status = 200, data = productDetails });
            }
            return NotFound(new { status = 404, message = "Product not found." });
        }




        [HttpGet]
        [Route("GetProductDetailsByid/{id}")]
        public async Task<IActionResult> GetProductDetailsByid(int id)
        {
            var productDetails = await productRepo.getProductwithdetailsByid(id);
            if (productDetails != null)
            {
                return Ok(new { status = 200, data = productDetails });
            }
            return NotFound(new { status = 404, message = "Product not found." });
        }
    }
}
