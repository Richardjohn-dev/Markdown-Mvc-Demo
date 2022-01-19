using MarkdownMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Models;

namespace MarkdownMvc.Controllers
{
    public class HomeController : Controller
    {
        public List<Post> Posts { get; set; }

        private readonly ILogger<HomeController> _logger;
        private readonly IFileService _fileService;

        public HomeController(ILogger<HomeController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
            Posts = _fileService.ReadAllPosts();
        }

        public IActionResult Index()
        {
            return View(Posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

  
}
