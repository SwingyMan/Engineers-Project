using System.Net.Mail;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;

namespace Application.Commands;

public class AddAttachmentCommandHandler : IRequestHandler<AddAttachmentCommand>
{
    private readonly IGenericRepository<Attachments> _attachmentRepository;
    private readonly IBlobInfrastructure _blobInfrastructure;
    private readonly IMapper _mapper;
    public AddAttachmentCommandHandler(IGenericRepository<Attachments> attachmentRepository, IBlobInfrastructure blobInfrastructure, IMapper mapper)
    {
        _mapper = mapper;
        _attachmentRepository = attachmentRepository;
        _blobInfrastructure = blobInfrastructure;
    }
    public Task Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
    {
        var mappedEntity = _mapper.Map<Attachments>(request.AttachmentDto);
        mappedEntity.FileName = $"{mappedEntity.Id}{Path.GetExtension(request.AttachmentDto.file.FileName)}";
        _blobInfrastructure.addBlob(request.AttachmentDto.file,mappedEntity.Id,request.AttachmentDto.FileType);
        var entity = _attachmentRepository.Add(mappedEntity);
        return entity;
    }
}