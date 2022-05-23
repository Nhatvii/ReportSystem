using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportSystemData.Dtos.Comment;
using ReportSystemData.Models;
using ReportSystemData.Response;
using ReportSystemData.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportSystemAPI.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        //private readonly CommentService _repository;
        //public CommentController(ReportSystemContext context)
        //{
        //    _repository = new CommentService(context);
        //}
        //[HttpGet]
        //[Produces("application/json")]
        //public ActionResult<Post> GetAllComment()
        //{
        //    return Ok(_repository.GetAllComment());
        //}

        //[HttpGet("{id}")]
        //[Produces("application/json")]
        //public ActionResult<Comment> GetCommentById(int id)
        //{
        //    var cmt = _repository.GetCommentById(id);
        //    if (cmt != null)
        //    {
        //        return Ok(cmt);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //[HttpPost]
        //public ActionResult<Post> CreateComment(CreateCommentDTO cmt)
        //{
        //    return Ok(_repository.CreateComment(cmt));
        //    //if (result != null)
        //    //{
        //    //    return Ok(result);
        //    //}
        //    //return NotFound("Create Error!!!");
        //}

        //[HttpPut("{id}")]
        //[Produces("application/json")]
        //public ActionResult UpdateComment(int id, UpdateCommentDTO cmt)
        //{
        //    var result = _repository.UpdateComment(id, cmt);
        //    if (result)
        //    {
        //        return Ok("Update Success!!!");
        //    }
        //    return NotFound("Update Error!!!");
        //}

        //[HttpDelete]
        //[Produces("application/json")]
        //public ActionResult DeleteComment(DeleteCommentDTO cmt)
        //{
        //    var result = _repository.DeleteComment(cmt);
        //    if (result)
        //    {
        //        return Ok("Delete Success!!!");
        //    }
        //    return NotFound("Delete Error!!!");
        //}
    }
}
