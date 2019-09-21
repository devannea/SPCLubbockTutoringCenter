using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPCLCTAPI.Core.Models;
using SPCLCTAPI.Core.Services;
using SPCLTCAPI.ApiModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPCLTCAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        
        // GET api/profile/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var profile = _profileService.Get(id);
            if (profile == null) return NotFound();
            // if the profile does not belong to the current user and the current user is not an admin
            if (profile.UserId != CurrentUserId && !User.IsInRole("Admin"))
            {
                ModelState.AddModelError("UserId", "You can only retrieve your own profile.");
                return BadRequest(ModelState);
            }
            return Ok(profile.ToApiModel());
        }

        // GET api/profiles
        private string CurrentUserId
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
        [HttpGet]
        public IActionResult Get()
        {
            // if the user is an Admin, return all activities
            if (User.IsInRole("Admin"))
            {
                var allProfiles = _profileService
                    .GetAll()
                    .ToApiModels();
                return Ok(allProfiles);
            }

            // otherwise return only the user's activities
            var profileModels = _profileService
                .GetAllForUser(CurrentUserId)
                .ToApiModels();
            return Ok(profileModels);
        }

        // POST api/profiles
        [HttpPost]
        public IActionResult Post([FromBody]Profile profile)
        {
            try
            {
                return Ok(_profileService.Add(profile).ToApiModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AddProfile", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT api/profiles/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Profile profile)
        {
            try
            {
                return Ok(_profileService.Update(profile).ToApiModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdateProfile", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE api/profiles/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _profileService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeleteProfile", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE /api/profiles
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete()
        {
            // code to delete all profiles goes here...

            return Ok("Deleted all profiles");
        }
    }
}
