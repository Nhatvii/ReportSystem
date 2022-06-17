using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Models;
using ReportSystemData.Parameters;
using ReportSystemData.Service;
using ReportSystemData.ViewModel.Post;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportSystemAPI.Controllers
{
    [Route("api/Post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _repository;
        public PostController(IPostService service)
        {
            _repository = service;
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<Post> GetAllPost([FromQuery] PostParameters postParameter)
        {
            return Ok(_repository.GetAllPost(postParameter));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public ActionResult<Post> GetPostById(string id)
        {
            return Ok(_repository.GetPostById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreatePost(CreatePostViewModel post)
        {
            return Ok(await _repository.CreatePostAsync(post));
        }

        [HttpPut]
        [Produces("application/json")]
        public ActionResult<Post> UpdatePost(UpdatePostViewModel post)
        {
            return Ok(_repository.UpdatePost(post));
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("EditIsPublic")]
        public ActionResult<Post> UpdatePublicPost(UpdatePublicPostViewModel post)
        {
            return Ok(_repository.UpdatePublicPost(post));
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public ActionResult DeletePost(string id)
        {
            return Ok(_repository.DeletePost(id));
        }

        [HttpPut]
        [Produces("application/json")]
        [Route("UpdateViewCount")]
        public async Task<IActionResult> UpdateViewCount(UpdateViewCountViewModel post)
        {
            return Ok(await _repository.UpdateViewCount(post));
        }
    }
}
