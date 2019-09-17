using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPCLCTAPI.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPCLTCAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TimesheetsController : ControllerBase
    {

        private readonly ITimesheetService _timesheetService;

        public TimesheetsController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpGet("/api/profiles/{profileId}/timesheets")]
        [AllowAnonymous]
        public IActionResult Get(int profileId)
        {
            var timesheets = _timesheetService.GetProfileTimesheets(profileId);
            var timesheetModels = timesheets.Select(t => t.ToApiModel());
            return Ok(timesheetModels);
        }

        // GET api/profiles/{profileId}/timesheets/{timesheetId}
        [HttpGet("/api/profiles/{profileId}/timesheets/{timesheetId}")]
        [AllowAnonymous]
        public IActionResult Get(int profileId, int timesheetId)
        {
            // TODO: replace the code below with the correct implementation
            var posts = _postService.GetBlogPosts(blogId);
            var post = posts.FirstOrDefault(p => p.Id == postId);
            var postModel = post.ToApiModel();
            return Ok(postModel);
        }

        // TODO: add a new post to blog
        // POST /api/blogs/{blogId}/post
        [HttpPost("/api/blogs/{blogId}/posts")]
        public IActionResult Post(int blogId, [FromBody]PostModel postModel)
        {
            // TODO: replace the code below with the correct implementation
            try
            {
                var post = postModel.ToDomainModel();
                _postService.Add(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("AddPost", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT /api/blogs/{blogId}/posts/{postId}
        [HttpPut("/api/blogs/{blogId}/posts/{postId}")]
        public IActionResult Put(int blogId, int postId, [FromBody]PostModel postModel)
        {
            try
            {
                var updatedPost = _postService.Update(postModel.ToDomainModel());
                return Ok(updatedPost);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UpdatePost", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // TODO: delete post by id
        // DELETE /api/blogs/{blogId}/posts/{postId}
        [HttpDelete("/api/blogs/{blogId}/posts/{postId}")]
        public IActionResult Delete(int blogId, int postId)
        {
            // TODO: replace the code below with the correct implementation
            try
            {
                _postService.Remove(postId);
                return NoContent();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("DeletePost", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
