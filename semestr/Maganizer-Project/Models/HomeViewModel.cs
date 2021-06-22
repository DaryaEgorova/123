using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maganizer_Project.Models
{
    public class HomeViewModel
    {
        public List<PostForHomeSliderModel> PostsSlider { get; set; } 
        public List<LatestPostModel> LatestPosts { get; set; }
    }

    public class PostForHomeSliderModel 
    {
        public string Name { get; set; }
        public byte[] FeaturedImage { get; set; }
        public string Tag { get; set; }
        public DateTime PostedOn { get; set; }
        public string AuthorName { get; set; }
    }

    public class LatestPostModel
    {
        public string Name { get; set; }
        public byte[] FeaturedImage { get; set; }
        public DateTime PostedOn { get; set; }
        public string AuthorName { get; set; }
    }
}
