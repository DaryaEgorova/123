using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.Models;
using Maganizer_Project.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Maganizer_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPostService postService;
        private readonly IAccountService accountService;
        private readonly ITagService tagService;

        public AdminController(IWebHostEnvironment hostingEnvironment, IPostService postService, IAccountService accountService, ITagService tagService)
        {
            _hostingEnvironment = hostingEnvironment;
            this.postService = postService;
            this.accountService = accountService;
            this.tagService = tagService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var users = accountService.GetInfoUsers();
            IEnumerable<TagDTO> tags = tagService.GetTags();

            if (users.Count() != 0)
            {
                AdminIndexViewModel adminIndexViewModel = new AdminIndexViewModel
                {
                    Users = new List<UserInfoDTO>(),
                    Tags = new List<TagDTO>()
                };

                foreach (var x in users)
                {
                    adminIndexViewModel.Users.Add(x);
                }

                if(tags.Count() != 0)
                {
                    adminIndexViewModel.Tags = tags.ToList();
                }

                return View("Index", adminIndexViewModel);
            }

            return View("Index");

        }
        [Authorize(Roles = "Admin, Manager")]
        [Route("Admin/PostEditor")]
        [HttpGet]
        public IActionResult PostEditor()
        {
            return View("PostEditor");
        }

        [Authorize(Roles = "Admin, Manager")]
        [Route("Admin/PostEditor")]
        [HttpPost]
        public IActionResult MakePost(AdminPostEditorViewModel editorViewModel)
        {
            if (ModelState.IsValid)
            {
                SaveNewImage(new List<IFormFile>() { editorViewModel.FeaturedImage });
                EditPostDTO postDTO = new EditPostDTO()
                {
                    Name = editorViewModel.PostName,
                    Content = editorViewModel.PostContent,
                    Tags = editorViewModel.Tags,
                    FeaturedImage = editorViewModel.FeaturedImage,
                    AuthorUsername = User.Identity.Name
                };             

                postService.AddPost(postDTO);

                editorViewModel.SuccessPost = true;
            }
            return View("PostEditor", editorViewModel);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        public async Task<IActionResult> SaveNewImage(List<IFormFile> file)
        {
            if(file == null)
            {
                var ex = new ArgumentException();
                return Json(ex.Message);
            }

            IFormFile theFile = file[0];

            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, "uploads");

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Basic validation on mime types and file extension
            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

            try
            {
                if ((Array.IndexOf(imageExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable imageUrl = new Hashtable
                    {
                        { "link", "/uploads/" + name }
                    };

                    return Json(imageUrl);
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteTag(string tag)
        {
            tagService.DeleteTag(tag);
            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("MessagesFromUsers")]
        public IActionResult MessagesFromUsers()
        {
            var messages = accountService.GetMessagesToAdmin();
            if(messages != null)
            {
                MessagesFromUsersViewModel messagesModel = new MessagesFromUsersViewModel()
                {
                    Messages = new List<MessageFromUser>()
                };

                foreach (var x in messages)
                {
                    messagesModel.Messages.Add(new MessageFromUser()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Subject = x.Subject,
                        AuthorName = x.Username,
                        SentOn = x.SentOn
                    });
                }
                messagesModel.Messages = messagesModel.Messages.OrderByDescending(x => x.SentOn).ToList();

                return View("MessagesFromUsers", messagesModel);
            }

            return View("MessagesFromUsers", new MessagesFromUsersViewModel() { Messages = new List<MessageFromUser>() });
        }

        [HttpPost]
        public IActionResult DeleteMessageFromUser(int id)
        {
            accountService.DeleteMessageToAdmin(id);
            return RedirectToAction("MessagesFromUsers");
        }
    }
}
