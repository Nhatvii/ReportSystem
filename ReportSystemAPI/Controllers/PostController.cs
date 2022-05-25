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

        //[HttpPut("{id}")]
        //[Produces("application/json")]
        //public ActionResult<Post> UpdatePost(int id, UpdatePost post)
        //{
        //    var result = _repository.UpdatePost(id, post);
        //    if (result)
        //    {
        //        return Ok("Update Success!!!");
        //    }
        //    return NotFound("Update Error!!!");
        //}
        //[HttpDelete("{id}")]
        //[Produces("application/json")]
        //public ActionResult<StatusCodeResult> DeletePost(int id)
        //{
        //    var result = _repository.DeletePost(id);
        //    if (result)
        //    {
        //        return Ok("Delete Success!!!");
        //    }
        //    return NotFound("Delete Error!!!");
        //}
    }
}
