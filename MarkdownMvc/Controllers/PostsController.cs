using Markdig;
using MarkdownMvc.Data;
using MarkdownMvc.Models;
using MarkdownMvc.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Models;

namespace MarkdownMvc.Controllers
{
    public class PostsController : Controller
    {

        
        public List<Post> Posts { get; set; }

        private readonly ILogger<PostsController> _logger;
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;

        public PostsController(ILogger<PostsController> logger, IFileService fileService, ApplicationDbContext context)
        {
            _logger = logger;
            _fileService = fileService;
            _context = context;
            Posts = _context.Posts.ToList();
        }


        // GET: PostController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Route("/post/{postTitle}")]
        public IActionResult Index(string postTitle)
        {
            var post = Posts.FirstOrDefault(p => p.Title == postTitle.Replace("-", " "));

            var pvm = new PostViewModel
            {
                Abstract = post.Abstract,
                Content = post.Content,
                Title = post.Title
            };
            return View(pvm);
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View(new PostViewModel());
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel pvm)
        {
            try
            {
                var post = new Post
                {
                    Abstract = pvm.Abstract,
                    Content = pvm.Content,
                    Title = pvm.Title
                };
                _context.Add(post);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new { postTitle = pvm.Title});
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var post = Posts.FirstOrDefault(p => p.Id == id);
            var pvm = new PostViewModel
            {
                Id = post.Id,
                Abstract = post.Abstract,
                Content = post.Content,
                Title = post.Title
            };
            return View(pvm);
           
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                var post = Posts.FirstOrDefault(p => p.Id == id);
                if (post is not null)
                {
                    post.Abstract = pvm.Abstract;
                    post.Content = pvm.Content;
                    post.Title = pvm.Title;
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index), new { postTitle = post.Title });
            }
            return View(pvm);           
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
