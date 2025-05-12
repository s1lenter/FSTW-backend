using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FSTW_backend
{
    public class UnauthorizedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            var userIdentity = actionExecutingContext.HttpContext.User.Identity;
            if (userIdentity is null) 
                return;
            if (userIdentity.IsAuthenticated)
                actionExecutingContext.Result = new BadRequestResult();
        }
    }
}
