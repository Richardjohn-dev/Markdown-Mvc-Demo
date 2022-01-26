using MarkdownMvc.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TheBlogProject.Models;
using Markdig;
using System.Text.RegularExpressions;

namespace MarkdownMvc
{
    public class FileService : IFileService
    {
        private readonly int _wordsPerMinute = 170;

        private static string _folder;

        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
            _folder = _env.WebRootPath + "/";

            EnsureFolder("/post-images/");
        }


        private static void EnsureFolder(string folder)
        {
            if (!Directory.Exists(Path.Combine(_folder, folder)))
            {
                Directory.CreateDirectory($"{_folder}{folder}/");
            }
        }

        public void ProcessRemovedImages(string oldBody, string newBody)
        {
            // were simply saying...
            // "we're going to delete these old images, unless they are in our list of links from newBody"
            var oldBodyImages = GetImageLinks(oldBody);
            if (oldBodyImages.Count > 0)
            {               
                var newBodyImages = GetImageLinks(newBody);
                var deleteImages = oldBodyImages.Except(newBodyImages).ToList();
                deleteImages.ForEach(x => DeleteImage(x));    
            }
        }

        public List<string> GetImageLinks(string body)
        {
            return  new Regex(@"!\[.*?\]\(.*?\)").Matches(body).Cast<Match>()
                .Select(m => m.Value.Substring(m.Value.LastIndexOf("/") + 1).Replace(")", "")).ToList();          
         }
        public void DeleteImage(string filename)
        {
            try
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "post-images", filename);
                if (File.Exists(file))
                {
                    //delete the file
                    File.Delete(file);
                }
            }
            catch
            {
                //todo log it maybe
            }
        }

        public string GetReadTime(string post)
        {
            //get the amount of words
            var words = post.Count(Char.IsWhiteSpace);

            //guess that people read 200 words per minute
            var timeToRead = words / _wordsPerMinute;

            return timeToRead < 1 ? "1" : timeToRead.ToString();
        }
    }
}