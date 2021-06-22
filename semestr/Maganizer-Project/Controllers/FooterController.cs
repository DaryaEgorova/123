using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.Extensions;
using Maganizer_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maganizer_Project.WEB.Controllers
{
    public class FooterController : Controller
    {
        private readonly ITagService tagService;
        private readonly IPostService postService;
        public FooterController(ITagService tagService, IPostService postService)
        {
            this.tagService = tagService;
            this.postService = postService;
        }

        [HttpGet("Footer/GetTags")]
        public ActionResult GetTags()
        {
            if (HttpRequestExtensions.IsAjaxRequest(Request))
            {
                IEnumerable<TagDTO> tags = tagService.GetTags();
                var tagsViewModel = new TagsViewModel()
                {
                    Tags = tags.ToList()
                };
                return PartialView("_TagsInfo", tagsViewModel);
            }

            return View("ErrorNotFound");

        }

        [HttpGet("Footer/GetPostEditorPicks")]
        public ActionResult GetPostEditorPicks()
        {
            if (HttpRequestExtensions.IsAjaxRequest(Request))
            {
                IEnumerable<GetPostDTO> posts = postService.GetPosts();

                var pickViewModel = new PostEditorPicksViewModel()
                {
                    Posts = new List<PostEditorPickModel>()
                };

                var authors = posts.Select(x => x.AuthorName).Distinct();

                foreach (var x in authors)
                {
                    var lastPostOfAuthor = posts.Where(y => y.AuthorName == x).OrderByDescending(y => y.DateOfCreation).FirstOrDefault();
                    pickViewModel.Posts.Add(new PostEditorPickModel()
                    {
                        Name = lastPostOfAuthor.Name,
                        FeaturedImage = lastPostOfAuthor.FeaturedImage,
                        PostedOn = lastPostOfAuthor.DateOfCreation,
                        AuthorName = lastPostOfAuthor.AuthorName
                    });
                }
                

                return PartialView("_PostEditorPickInfo", pickViewModel);
            }

            return View("ErrorNotFound");

        }
    }
}
