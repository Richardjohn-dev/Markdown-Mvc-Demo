using MarkdownMvc.Models;
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

        public PostsController(ILogger<PostsController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
            Posts = _fileService.ReadAllPosts();
        }


        // GET: PostController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Route("/post/{postTitle}")]
        public IActionResult Index(string postTitle)
        {
            return View(
                Posts.FirstOrDefault(p => p.Title == postTitle.Replace("-", " "))
            );
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
