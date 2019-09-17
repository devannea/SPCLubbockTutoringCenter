using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPCLCTAPI.Core.Models;
using SPCLCTAPI.Core.Services;

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
        // GET: api/profiles
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                var profile = _profileService.Get(id);
                var blogModels = profile.ToApiModel();
                return Ok(profileModels);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetProfile", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // GET api/profiles/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get()
        {
            try
            {
                var profiles = _profileService.GetAll();
                var profileModels = profiles.Select(p => p.ToApiModel());
                return Ok(profileModels);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("GetProfiles", ex.Message);
                return BadRequest(ModelState);
            }
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
    }
}
