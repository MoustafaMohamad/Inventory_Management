using AutoMapper.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Inventory_Management.Common.Middlewares
{
    public class CustomizedAuthorizeAttribute : ActionFilterAttribute
    {
        
      private  UserState _userState;

        public CustomizedAuthorizeAttribute(UserState userState)
        {
            _userState = userState;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loggedUser = context.HttpContext.User;

            var roleID = loggedUser.FindFirst("RoleID");

            if (roleID is null || string.IsNullOrEmpty(roleID.Value))
            {
                throw new UnauthorizedAccessException();
            }

            bool hasAccess = true;  //= _roleFeatureService.HasAccess(int.Parse(roleID.Value), _feature);

            if (!hasAccess)
            {
                throw new UnauthorizedAccessException();
            }

            _userState.Role = roleID.Value;
            _userState.ID = loggedUser.FindFirst("ID")?.Value ?? "";
            _userState.Name = loggedUser.FindFirst(ClaimTypes.Name)?.Value ?? "";

            base.OnActionExecuting(context);
        }
    }
}
