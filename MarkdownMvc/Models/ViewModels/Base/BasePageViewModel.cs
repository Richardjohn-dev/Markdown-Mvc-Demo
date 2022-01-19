using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownMvc.Models.ViewModels.Base
{
    public class BasePageViewModel
    {
        public string BlogName { get; set; }

        public string TagLine { get; set; }

        public string WindowTitle { get; set; }

        public string MetaDescription { get; set; }

        public bool DontIndexPage { get; set; }
    }
}
