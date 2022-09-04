using System.Net;
using ExampleApi.Infrastructure;
using ExampleApi.Models;
using ExampleApi.Models.Inputs;
using ExampleApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController :  ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    public CommentController(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    [Route("")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CommentViewModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetComments(CancellationToken cancellationToken)
    {
        var comments = await _databaseContext.Comments.ToListAsync(cancellationToken);
    
        return Ok(comments.Select(Mapper.MapCommentToViewModel).ToList());
    }

    [Route("{id:guid}")]
    [HttpGet]
    [ProducesResponseType(typeof(CommentViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetComment(Guid id)
    {
        var comment = await _databaseContext.Comments.SingleOrDefaultAsync(x=> x.Id == id);
    
        return Ok(Mapper.MapCommentToViewModel(comment!));
    }
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(CreateCommentViewModel), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateComment(CreateCommentInput request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Message = request.Message,
            Author = request.Author,
            Date = request.Date,
            Type = request.Type
        };
        
        await _databaseContext.Comments.AddAsync(comment, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return Created(string.Empty, new CreateCommentViewModel {CommentId = comment.Id});
    }
}