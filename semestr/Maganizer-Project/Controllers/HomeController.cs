using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.Extensions;
using Maganizer_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maganizer_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IAccountService accountService;
        public HomeController(IPostService postService, ITagService tagService, IAccountService accountService)
        {
            this.postService = postService;
            this.tagService = tagService;
            this.accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {      
            HomeViewModel homeView = new HomeViewModel()
            {
                PostsSlider = new List<PostForHomeSliderModel>()
            };

            var posts = postService.GetPosts();

            if (posts.Count() != 0)
            {
                Random rnd = new Random();
                int[] usedNumbers = { -1, -1, -1, -1, -1 };
                int n = usedNumbers.Length;
                if (posts.Count() < n)
                {
                    n = posts.Count();
                }
                for (int i = 0; i < n;)
                {
                    int number = rnd.Next(0, posts.Count());
                    if (usedNumbers.Contains(number))
                    {
                        continue;
                    }

                    usedNumbers[i] = number;

                    string tag = "";

                    if (posts.ElementAt(number).Tags.Count() != 0)
                    {
                        tag = posts.ElementAt(number).Tags.FirstOrDefault().Name;
                    }

                    homeView.PostsSlider.Add(new PostForHomeSliderModel()
                    {
                        Name = posts.ElementAt(number).Name,
                        Tag = tag,
                        FeaturedImage = posts.ElementAt(number).FeaturedImage,
                        PostedOn = posts.ElementAt(number).DateOfCreation,
                        AuthorName = posts.ElementAt(number).AuthorName
                    });

                    

                    i++;
                }

                posts = posts.OrderByDescending(x => x.DateOfCreation).ToList();
                homeView.LatestPosts = new List<LatestPostModel>();
                n = 20;
                if (posts.Count() < n)
                {
                    n = posts.Count();
                }
                for(int i=0; i < n; i++)
                {
                    homeView.LatestPosts.Add(new LatestPostModel()
                    {
                        Name = posts.ElementAt(i).Name,
                        PostedOn = posts.ElementAt(i).DateOfCreation,
                        FeaturedImage = posts.ElementAt(i).FeaturedImage,
                        AuthorName = posts.ElementAt(i).AuthorName
                    });
                }

                return View("Home", homeView);
            }
            return View("Home");
        }

        [HttpGet("Home/GetLatestTags")]
        public ActionResult GetLatestTags()
        {
            if (HttpRequestExtensions.IsAjaxRequest(Request))
            {
                IEnumerable<TagDTO> tags = tagService.GetTags();
                var tagsViewModel = new TagsViewModel()
                {
                    Tags = tags.ToList()
                };

                tagsViewModel.Tags = tagsViewModel.Tags.OrderByDescending(x => x.DateOfCreation).ToList();

                return PartialView("_LatestTagsInfo", tagsViewModel);
            }

            return View("ErrorNotFound");

        }

        [Authorize]
        [HttpGet("ContactUs")]
        public IActionResult ContactUs()
        {
            return View("ContactUs");
        }

        [Authorize]
        [HttpPost("ContactUs")]
        public IActionResult SendContactMessage(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                MessageToAdminDTO contactUsMessage = new MessageToAdminDTO()
                {
                    Content = model.Message,
                    Subject = model.Subject,
                    Username = User.Identity.Name,
                    SentOn = DateTime.Now
                };

                accountService.CreateMessageToAdmin(contactUsMessage);

                model.Success = true;

                return View("ContactUs", model);
            }

            return View("ContactUs", model);
        }
    }
}
