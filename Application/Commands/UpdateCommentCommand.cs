using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class UpdateCommentCommand : IRequest<Comment>
{
    
    public CommentDTO CommentDto { get; set; }
    public Guid UserId { get; set; }

    public UpdateCommentCommand(CommentDTO commentDto, Guid userId)
    {
        CommentDto = commentDto;
        UserId = userId;
    }
}