using Microsoft.AspNetCore.Mvc.Filters;

namespace Study.VS2022.WebAPI.Filters
{
    public class TestFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            /*
             context.HttpContext.Request.Headers["Authorization"]
{}
    Count: 0
    结果视图: 展开结果视图将会枚举 IEnumerable
context.HttpContext.Request.Path
{/api/AR1/AH1/Login}
    HasValue: true
    Value: "/api/AR1/AH1/Login"
             */

            // 用户名：
            string currentUserName = context.HttpContext.User.Identity.Name;



            base.OnActionExecuting(context);
        }
    }
}
