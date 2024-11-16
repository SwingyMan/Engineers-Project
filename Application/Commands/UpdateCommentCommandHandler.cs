using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand,Comment>
{
    private readonly SocialPlatformDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCommentCommandHandler(SocialPlatformDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Comment> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(request);
        comment.UserId = request.UserId; 
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync(cancellationToken);
        return comment;
    }
}