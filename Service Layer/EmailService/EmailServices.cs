using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service_Layer.IEmail;
using Data_Layer.Utilities;

namespace Service_Layer.EmailService
{
    public class EmailServices(IUrlHelperFactory factory, IHttpContextAccessor accessor) : IEmailService
    {
        public OperationResult<string> GenerateLink(string userId, params string[] routeValues)
        {
            try
            {
                var ctx = accessor.HttpContext;

                var actionCtx = new ActionContext(ctx, ctx.GetRouteData(), new ActionDescriptor());
                var urlHelper = factory.GetUrlHelper(actionCtx);
                var url = urlHelper.Action(
                    action: "ConfirmEmail",
                    controller: "Auth",
                    values: new { userId, value = string.Join("&", routeValues) },
                    protocol: ctx.Request.Scheme
                );

                return OperationResult<string>.SuccessResult(url);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }
    }
}
