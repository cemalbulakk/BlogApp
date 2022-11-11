using BlogApp.Application.Features.Dtos;
using BlogApp.Application.Features.Enums;
using BlogApp.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Services;

public class PermissionFilter : IActionFilter
{
    private readonly IRoleService _roleService;
    public PermissionFilter(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.HttpContext.Request.Headers["UserId"].FirstOrDefault();
        //Role Yetkisine bakılır.
        if (HasRoleAttribute(context))
        {
            try
            {
                var arguments = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.CustomAttributes
                    .FirstOrDefault(fd => fd.AttributeType == typeof(RoleAttribute))?.ConstructorArguments;

                string? roleGroupId = (string)(arguments?[0].Value)!;
                Int64 bitwiseId = (Int64)(arguments?[1].Value ?? 0);
                RoleModel role = _roleService.GetRoleById(userId, roleGroupId, bitwiseId).Data;
                if (role != null && string.IsNullOrWhiteSpace(role.Id))
                {
                    //Forbidden 403 Result. Yetkiniz Yoktur..
                    context.Result = new ObjectResult(context.ModelState)
                    {
                        Value = "You are not authorized for this page",
                        StatusCode = StatusCodes.Status403Forbidden
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public bool HasRoleAttribute(FilterContext context)
    {
        return ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.CustomAttributes.Any(filterDescriptors => filterDescriptors.AttributeType == typeof(RoleAttribute));
    }
}