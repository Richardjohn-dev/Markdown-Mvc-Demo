using System.Collections.Generic;
using TheBlogProject.Models;

namespace MarkdownMvc
{
    public interface IFileService
    {
        List<Post> ReadAllPosts();
    }
}