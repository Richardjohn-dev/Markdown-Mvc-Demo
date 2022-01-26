using Markdig;
using MarkdownMvc.Data;
using MarkdownMvc.Models;
using MarkdownMvc.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoSauce.MagicScaler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly IWebHostEnvironment _env;

        public PostsController(ILogger<PostsController> logger, IFileService fileService, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _fileService = fileService;
            _context = context;
            _env = env;
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
                var originalPost = Posts.FirstOrDefault(p => p.Id == id);
                if (originalPost is not null)
                {

                    //first we delete abandoned images
                    _fileService.ProcessRemovedImages(originalPost.Content, pvm.Content);

                    originalPost.Abstract = pvm.Abstract;
                    originalPost.Content = pvm.Content;
                    originalPost.Title = pvm.Title;
                    originalPost.ReadTime = _fileService.GetReadTime(pvm.Content);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index), new { postTitle = originalPost.Title });
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
               
        [HttpPost]
        public JsonResult UploadImage(IFormFile file)
        {
            if (file is not null)
            {
                string fileName = $"{Guid.NewGuid()}-{file.FileName.Replace(" ", "-")}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "post-images", fileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                //await file.CopyToAsync(stream);
                MagicImageProcessor.ProcessImage(file.OpenReadStream(), fileStream, ImageOptions());

                return new JsonResult(fileName);
            }

            return Json(new { ErrorMessage = "You need to be logged in to upload images." }); ;
        }


        private static ProcessImageSettings ImageOptions() => new()
        {
            Width = 800,
            Height = 500,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };

    }
}
