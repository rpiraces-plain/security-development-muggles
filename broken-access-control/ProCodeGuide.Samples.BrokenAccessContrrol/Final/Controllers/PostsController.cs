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
        private readonly IPostsService? _postsService;

        private readonly IHashids? _hashids;

        public PostsController(IPostsService postsService, IHashids hashids)
        {
            _postsService = postsService;
            _hashids = hashids;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var posts = _postsService.GetAll(userId)
                .Select(x =>  PostViewModel.From(x, _hashids))
                .ToList();
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
            var postViewModel = PostViewModel.From(_postsService.GetById(id), _hashids);
            return View(postViewModel);
        }
    }
}
