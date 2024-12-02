using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand,Comment>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;

    public AddCommentCommandHandler(SocialPlatformDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Comment> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(request.CommentDto);
        comment.UserId = request.UserId; 
        comment.CreatedDate = DateTime.Now;
        var users = await _context.Users.SingleAsync(x => x.Id == request.UserId);
        comment.User = users;
        await _context.Comments.AddAsync(comment,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return comment;
    }
}