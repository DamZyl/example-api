using System.Net;
using ExampleApi.Infrastructure;
using ExampleApi.Models;
using ExampleApi.Models.Enums;
using ExampleApi.Models.Exceptions;
using ExampleApi.Models.Inputs;
using ExampleApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValidationException = ExampleApi.Models.Exceptions.ValidationException;

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
    
    [Route("Exception/Internal")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult ThrowInternalException(CancellationToken cancellationToken)
        => throw new InternalServerException();
    
    [Route("Exception/NotFound")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult ThrowNotFoundException(CancellationToken cancellationToken)
        => throw new NotFoundException();
    
    [Route("Exception/Unhandled")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult ThrowUnhandledException(CancellationToken cancellationToken)
        => throw new UnhandledException();
    
    [Route("Exception/Validation")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult ThrowValidationException(CancellationToken cancellationToken)
        => throw new ValidationException();
    
    [Route("{commentId:guid}")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> UpdateComment(Guid commentId, CancellationToken cancellationToken)
    {
        var comment = await _databaseContext.Comments.Where(x => x.Id == commentId)
            .SingleOrDefaultAsync(cancellationToken);

        if (comment is null)
        {
            throw new Exception("Error.");
        }

        comment.Type = comment.Type == CommentType.Positive ? CommentType.Negative : CommentType.Positive;

        _databaseContext.Comments.Update(comment);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}