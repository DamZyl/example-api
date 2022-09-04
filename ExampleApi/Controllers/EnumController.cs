using System.Net;
using ExampleApi.Infrastructure;
using ExampleApi.Models.Enums;
using ExampleApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnumController : ControllerBase
{
    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EnumViewModel>), (int)HttpStatusCode.OK)]
    public IActionResult GetCommentTypes(CancellationToken cancellationToken)
    {
        var enums = Enum.GetValues<CommentType>().ToList();

        return Ok(enums.Select(Mapper.MapEnumToViewModel));
    }
}