using AutoMapper;
using Domain.Entities;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using MediatR;

namespace Application.Commands;

public class RemoveAttachmentCommandHandler : IRequestHandler<RemoveAttachmentCommand>
{   private readonly IGenericRepository<Attachments> _attachmentRepository;
    private readonly IBlobInfrastructure _blobInfrastructure;
    private readonly IMapper _mapper;
    public RemoveAttachmentCommandHandler(IGenericRepository<Attachments> attachmentRepository, IBlobInfrastructure blobInfrastructure, IMapper mapper)
    {
        _mapper = mapper;
        _attachmentRepository = attachmentRepository;
        _blobInfrastructure = blobInfrastructure;
    }

    public async Task Handle(RemoveAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _attachmentRepository.GetByID(request.AttachmentId);
        await _attachmentRepository.Delete(request.AttachmentId);
        await _blobInfrastructure.deleteBlob(entity.FileName,entity.Type);
     }
}