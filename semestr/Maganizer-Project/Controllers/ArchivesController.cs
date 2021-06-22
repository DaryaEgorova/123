using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Maganizer_Project.Controllers
{
    public class ArchivesController : Controller
    {
        private readonly ITagService tagService;
        public ArchivesController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet("Tag")]
        public IActionResult GetTag(string name)
        {
            var tagDTO = tagService.GetTag(name);
            
            if(tagDTO != null)
            {
                TagArchiveViewModel tagViewModel = new TagArchiveViewModel()
                {
                    TagName = tagDTO.Name,
                    Posts = new List<PostOfTagModel>()
                };

                foreach(var x in tagDTO.Posts) 
                {
                    tagViewModel.Posts.Add(new PostOfTagModel()
                    {
                        PostName = x.PostName,
                        FirstTagName = x.FirstTagName,
                        PostedOn = x.PostedOn,
                        FeaturedPostImage = x.FeaturedPostImage,
                        AuthorName = x.AuthorName
                    });
                }

                return View("TagsArchive", tagViewModel);
            }

            return View("ErrorNotFound");
        }
    }
}
