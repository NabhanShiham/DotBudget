using DotBudget.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotBudget.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService categoryService;
        public CategoryController(CategoryService _categoryService) {
            categoryService = _categoryService;
        }

        [HttpGet("{id}")]
        public ActionResult GetCategory(string id) {
            try
            {
                var category = categoryService.GetCategory(id);
                return Ok(category);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult AddCategory([FromBody] Category category) {
            if (!ModelState.IsValid) {
                return BadRequest("Incorrect model state!");
            }
            try
            {
                categoryService.AddCategory(category);
                return Ok(category);
            }
            catch (Exception ex) {
                return BadRequest($"A crazy exception has occurred when adding a category cuh {ex}");
            }
        }

        [HttpDelete]
        public ActionResult DeleteCategory(string id) {
            try
            {
                categoryService.DeleteCategory(id);
                return Ok("Deleted da Category cuh");
            }
            catch (Exception ex) {
                return BadRequest($"A crazy exception has occurred when deleting yo category cuh {ex}");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult UpdateCategory([FromBody] Category updatedCategory) {
            if (!ModelState.IsValid) { 
                return BadRequest("Incorrect model state for a category cuh!");
            }
            try
            {
                categoryService.UpdateCategory(updatedCategory);
                return Ok($"Updated yo category cuh {updatedCategory}");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }


    }
}
