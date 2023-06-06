using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProCodeGuide.Samples.BrokenAccessControl.Models;
using ProCodeGuide.Samples.BrokenAccessControl.Services;
using System.Security.Claims;
using HashidsNet;
using ProCodeGuide.Samples.BrokenAccessControl.Infrastructure.HashIds;
using ProCodeGuide.Samples.BrokenAccessControl.ViewModels;

namespace ProCodeGuide.Samples.BrokenAccessControl.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostsService? _postsService = null;

        private readonly IHashids _hashids = null;

        public PostsController(IPostsService postsService, IHashids hashids)
        {
            _postsService = postsService;
            _hashids = hashids;
        }

        public IActionResult Index()
        {
            var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // TODO: Refactor
            List<PostViewModel> posts = _postsService.GetAll(userId)
                .Select(x => new PostViewModel
                {
                    Id = _hashids.Encode(x.Id.GetValueOrDefault()),
                    CreatedOn = x.CreatedOn,
                    Description = x.Description,
                    Title = x.Title
                }).ToList();
            return View(posts);
        }

        public IActionResult Create()
        {
            return View(new Post());
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _postsService.Create(post, userId);
            return RedirectToAction("Index");
        }

        public IActionResult Details([ModelBinder(typeof(HashidsModelBinder))] int id)
        {
            // TODO: Refactor
            var post = _postsService.GetById(id);
            var postViewModel = new PostViewModel
            {
                Id = _hashids.Encode(post.Id.GetValueOrDefault()),
                CreatedOn = post.CreatedOn,
                Description = post.Description,
                Title = post.Title
            };
            return View(postViewModel);
        }
    }
}
