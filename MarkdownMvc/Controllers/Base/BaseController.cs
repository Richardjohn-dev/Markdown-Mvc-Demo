using MarkdownMvc.Models.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownMvc.Controllers.Base
{
    public class BaseController : Controller
    {
        public BasePageViewModel Model { get; set; }

    
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Model = (BasePageViewModel) context.ActionArguments?.FirstOrDefault(a => a.Value is BasePageViewModel).Value;

            //only continue if we have a model
            if (Model != null)
            {
                //load settings from cache
                //var settings = SettingsCache.GetSettings();

                ////set defaults
                //Model.BlogName = settings.BlogName;
                //Model.TagLine = settings.TagLine;
                //Model.WindowTitle = settings.DefaultWindowTitle;
                //Model.MetaDescription = settings.DefaultMetaDescription;
            }
        }
    }
}
