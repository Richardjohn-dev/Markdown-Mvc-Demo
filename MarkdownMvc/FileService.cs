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
            //get all old links
            var allOldLinks = new Regex(@"!\[.*?\]\(.*?\)").Matches(oldBody).Cast<Match>().Select(m => m.Value).ToList();

            //check that there is something to remove
            if (allOldLinks.Count > 0)
            {
                //parse all old links to get the guids
                var oldLinkGuidList = new List<string>();
                foreach (var oldLink in allOldLinks)
                {
                    //make sure the link is for this server
                    if (oldLink.Contains("/post-images/"))
                    {
                        //build a list of all old images
                        oldLinkGuidList.Add(oldLink.Substring(oldLink.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase)));
                    }
                }

                //get all new links
                var allNewLinks = new Regex(@"!\[.*?\]\(.*?\)").Matches(newBody).Cast<Match>().Select(m => m.Value).ToList();
                foreach (var newLink in allNewLinks)
                {
                    //make sure the link is for this server
                    if (newLink.Contains("/post-images/"))
                    {
                        //remove all images still in use from the list
                        oldLinkGuidList.Remove(newLink.Substring(newLink.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase)));
                    }
                }

                //once we are done adding and removing to the list, delete all that remain from disk
                if (oldLinkGuidList.Count > 0)
                {
                    foreach (var image in oldLinkGuidList)
                    {
                        //delete the image file
                        DeleteImage(image.Replace("/", "").Replace(")", ""));
                    }
                }
            }
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

    }
}