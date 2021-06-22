using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Hubs
{
    public class CommentHub : Hub
    {
        private readonly IUserProfileService profileService;
        private readonly ICommentService commentService;
        public CommentHub(IUserProfileService profileService, ICommentService commentService) 
        {
            this.profileService = profileService;
            this.commentService = commentService;
        }

        [Authorize]
        public async Task SendComment(string username, string message, string postid, string id)
        {
            var profile = await profileService.GetProfile(username);

            var avatar = profile.Avatar;
            var sendOn = DateTime.Now;

            CommentDTO commentDTO = new CommentDTO()
            {
                Content = message,
                AuthorName = username,
                AuthorAvatar = avatar,
                PostId = Int32.Parse(id),
                PostDate = sendOn
            };

            await commentService.AddComment(commentDTO);

            string date = $"{sendOn.ToString("MMM", CultureInfo.InvariantCulture)} {sendOn.Day:00}, {sendOn.Year} at {sendOn:HH:mm:ss}";

            await Clients.All.SendAsync("ReceiveComment", username, message, avatar, date, postid);
        }
        
    }


}
