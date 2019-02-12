using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TCABS.Util
{
    // https://stackoverflow.com/questions/39870298/how-do-i-specify-different-layouts-in-the-asp-net-core-mvc
    public sealed class ViewLayoutAttribute : ResultFilterAttribute
    {
        private readonly string _layout;
        public ViewLayoutAttribute(string layout)
        {
            _layout = layout;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult viewResult)
            {
                viewResult.ViewData["Layout"] = _layout;
            }
        }
    }
}
