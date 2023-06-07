using HashidsNet;
using ProCodeGuide.Samples.BrokenAccessControl.Models;

namespace ProCodeGuide.Samples.BrokenAccessControl.ViewModels
{
    public class PostViewModel
    {
        public string? Id { set; get; }
        public string? Title { set; get; }
        public string? Description { set; get; }
        public DateTime? CreatedOn { get; set; }

        public static PostViewModel? From(Post? post, IHashids hashids)
        {
            if (post is null) return null;
            return new PostViewModel
            {
                Id = hashids.Encode(post.Id.GetValueOrDefault()),
                Title = post.Title,
                Description = post.Description,
                CreatedOn = post.CreatedOn
            };
        }
    }
}
