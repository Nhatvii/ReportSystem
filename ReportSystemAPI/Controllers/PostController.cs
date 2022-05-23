using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Models;
using ReportSystemData.Service;
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
        public ActionResult<Post> GetAllPost()
        {
            return Ok(_repository.GetAllPost());
        }

        //[HttpGet("{id}")]
        //[Produces("application/json")]
        //public ActionResult<Post> GetPostById(int id)
        //{
        //    var post = _repository.GetPostById(id);
        //    if (post != null)
        //    {
        //        return Ok(post);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpPost]
        //public ActionResult<Post> CreatePost(CreatePost post)
        //{
        //    var result = _repository.CreatePost(post);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound("Create Error!!!");
        //}
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
