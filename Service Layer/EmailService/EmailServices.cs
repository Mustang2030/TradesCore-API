using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data_Layer.Utilities;

namespace Service_Layer.EmailService
{
    public class EmailServices(IUrlHelperFactory factory, IHttpContextAccessor accessor) : IEmailService.IEmailService
    {

        /// <summary>
        /// Generates a link for the user to confirm their email address.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is to be confirmed.
        /// </param>
        /// <param name="actionMethod">
        /// The name of the action method to be called when the link is clicked.
        /// </param>
        /// <param name="controller">
        /// The name of the controller to be called when the link is clicked.
        /// </param>
        /// <param name="token">
        /// The email confirmation token to be included in the link.
        /// </param>
        /// <returns>
        /// The result of the operation, including the generated link if successful.
        /// </returns>
        public OperationResult<string> GenerateLink(string userId, string actionMethod, string controller, string token)
        {
            try
            {
                var ctx = accessor.HttpContext;

                var actionCtx = new ActionContext(ctx, ctx.GetRouteData(), new ActionDescriptor());
                var urlHelper = factory.GetUrlHelper(actionCtx);
                var url = urlHelper.Action(
                    action: actionMethod,
                    controller: controller,
                    values: new { userId, token },
                    protocol: ctx.Request.Scheme
                );

                return OperationResult<string>.SuccessResult(url);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Failure(ex.Message + "\nInner Exception: " + ex.InnerException);
            }
        }

        /// <summary>
        /// Generates a link for the user to confirm their updated email address.
        /// </summary>
        /// <param name="userId">
        /// The unique id of the user whose email address is to be confirmed.
        /// </param>
        /// <param name="actionMethod">
        /// The name of the action method to be called when the link is clicked.
        /// </param>
        /// <param name="controller">
        /// The name of the controller to be called when the link is clicked.
        /// </param>
        /// <param name="token">
        /// The email confirmation token to be included in the link.
        /// </param>
        /// <param name="newEmail">
        /// The new email address to be confirmed.
        /// </param>
        /// <returns>
        /// The result of the operation, including the generated link if successful.
        /// </returns>
        public OperationResult<string> GenerateLink(string userId, string actionMethod, string controller, string token, string newEmail)
        {
            try
            {
                var ctx = accessor.HttpContext;

                var actionCtx = new ActionContext(ctx, ctx.GetRouteData(), new ActionDescriptor());
                var urlHelper = factory.GetUrlHelper(actionCtx);
                var url = urlHelper.Action(
                    action: actionMethod,
                    controller: controller,
                    values: new { userId, token, newEmail },
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
