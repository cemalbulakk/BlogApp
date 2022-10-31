using BlogApp.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Common.BaseController;

public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResult<T>(Response<T> response)
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}