using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Message>
{
    private readonly IMessageRepository _repository;
    private readonly IMapper _mapper;

    public UpdateMessageCommandHandler(IMessageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Message> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Message>(request.entity);
        return await _repository.Update(mapped);
    }
}

