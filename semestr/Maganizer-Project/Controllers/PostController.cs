using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maganizer_Project.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICommentService commentService;
        public PostController(IPostService postService, ICommentService commentService)
        {
            this.postService = postService;
            this.commentService = commentService;
        }

        [HttpGet("Post")]
        public IActionResult GetPost(string name)
        {
            var post = postService.GetPostByName(name);

            if(post == null)
            {
                return View("ErrorNotFound");
            }

            var comments = commentService.GetCommentsOfPost(post.Id);

            PostViewModel postViewModel = new PostViewModel()
            {
                Id = post.Id,
                PostId = "post-"+post.Id,
                Name = post.Name,
                Content = post.Content,
                PostedOn = post.DateOfCreation,
                FeaturedImage = post.FeaturedImage,
                NextPost = post.NextPostName,
                PrevPost = post.PreviousPostName,
                AuthorName = post.AuthorName
            };

            if(comments.Count() != 0)
            {
                postViewModel.Comments = new List<CommentViewModel>();
                foreach (var x in comments)
                {

                    postViewModel.Comments.Add(new CommentViewModel()
                    {
                        Content = x.Content,
                        AuthorName = x.AuthorName,
                        AuthorAvatar = x.AuthorAvatar,
                        PostDate = $"{x.PostDate.ToString("MMM", CultureInfo.InvariantCulture)} {x.PostDate.Day:00}, {x.PostDate.Year} at {x.PostDate:HH:mm:ss}"
                    });
                }

                postViewModel.Comments = postViewModel.Comments.OrderByDescending(x => x.PostDate).ToList();
            }         

            postViewModel.Tags = new List<string>(post.Tags.Count());

            foreach (var x in post.Tags)
            {
                postViewModel.Tags.Add(x.Name);
            }
            return View("Post", postViewModel);
        }
    }
}
