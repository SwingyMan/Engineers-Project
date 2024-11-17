using Application.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class AddCommentCommand : IRequest<Comment>
{
    public CommentDTO CommentDto { get; set; }
    public Guid UserId { get; set; }

    public AddCommentCommand(CommentDTO commentDto, Guid userId)
    {
        CommentDto = commentDto;
        UserId = userId;
    }
}