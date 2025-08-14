using DotBudget.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotBudget.Data.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService profileService;

        public ProfileController(ProfileService _profileService) {
            profileService = _profileService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProfileById(string id)
        {
            try
            {
                var profile = profileService.GetProfileById(id);
                return Ok(profile);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Consumes("application/json")]
        public ActionResult CreateProfile([FromBody] Profile profile)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            profileService.AddProfile(profile);
            return CreatedAtAction(nameof(GetProfileById), new { id = profile.Id }, profile);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProfile(string id)
        {
            try
            {
                profileService.DeleteProfile(id);
                return NoContent(); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to delete profile"); 
            }
        }


    }
}
