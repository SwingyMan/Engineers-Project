using AutoMapper;
using Domain.Entities;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Message>
{
    private readonly IGenericRepository<Message> _genericRepository;
    private readonly IMapper _mapper;

    public UpdateMessageCommandHandler(IGenericRepository<Message> genericRepository, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<Message> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<Message>(request.entity);
        return await _genericRepository.Update(request.id, mapped);
    }
}

