using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Salary.App.Utils.Base.Response;

namespace Salary.App.Utils
{
    public class PrivateBaseController : Controller
    {
        public readonly ILogger<PrivateBaseController> logger;

        public PrivateBaseController(ILogger<PrivateBaseController> logger)
        {
            this.logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var session = context.HttpContext.Session;

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    Response response = new Response(true, new Metadata(-2, "Sesion invalida"));

                    context.Result = new JsonResult(response)
                    {
                        StatusCode = 401 // Unauthorized status code
                    };
                }
                else
                {
                    context.Result = RedirectToAction("Index", "Home");
                }

            }
            

            base.OnActionExecuting(context);
        }
    }
}
