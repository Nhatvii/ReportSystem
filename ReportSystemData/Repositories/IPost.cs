using ReportSystemData.Dtos;
using ReportSystemData.Dtos.Post;
using ReportSystemData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportSystemData.Repository
{
    interface IPost
    {
        IEnumerable<Post> GetAllPost();
        //Post GetPostById(int id);
        //Post CreatePost(CreatePost post);
        //bool UpdatePost(int id, UpdatePost updatePost);
        //bool DeletePost(int id);

    }
}
