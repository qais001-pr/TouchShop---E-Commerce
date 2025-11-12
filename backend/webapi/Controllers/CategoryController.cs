using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Repository;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly CategoryRepo? _categoryRepo;
        public CategoryController(CategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }


        [HttpPost]
        [Route("addCategory")]

        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.category_name) || string.IsNullOrEmpty(category.description))
            {
                return BadRequest("Category name cannot be null or empty.");
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            bool updated = await _categoryRepo.AddCategory(category);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (updated)
                return Ok(new { status = 200, message = "Category Added Successfully" });
            return BadRequest("Category already exists.");
        }


        [HttpGet]
        [Route("GetAllCategory")]

        public async Task<IActionResult> GetAllCategory()
        {
            List<Category> categoriesList = new List<Category>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            categoriesList = await _categoryRepo.ShowAllCategory();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return Ok(new { status = 200, data = categoriesList });
        }
        [HttpPut]
        [Route("updateCategory")]

        public async Task<IActionResult> updateCategory([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.category_name) || string.IsNullOrEmpty(category.description))
            {
                return BadRequest("Category name cannot be null or empty.");
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _categoryRepo.updateCategory(category);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return Ok(new { status = 200, message = "Category Updated Successfully" });
        }

        [HttpDelete]
        [Route("DeleteCategory")]

        public async Task<IActionResult> deleteCategory([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.category_name) || string.IsNullOrEmpty(category.description))
            {
                return BadRequest("Category name cannot be null or empty.");
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await _categoryRepo.DeleteCategory(category);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return Ok(new { status = 200, message = "Category Deleted Successfully" });
        }
    }
}
