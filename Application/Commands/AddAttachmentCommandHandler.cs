using System.Net.Mail;
using Application.DTOs;
using AutoMapper;
using Azure.AI.ContentSafety;
using Domain.Entities;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands;

public class AddAttachmentCommandHandler : IRequestHandler<AddAttachmentCommand,Attachments>
{
    private readonly IGenericRepository<Attachments> _attachmentRepository;
    private readonly IBlobInfrastructure _blobInfrastructure;
    private readonly IMapper _mapper;
    private readonly ContentSafetyClient _contentSafetyClient;
    public AddAttachmentCommandHandler(IGenericRepository<Attachments> attachmentRepository, IBlobInfrastructure blobInfrastructure, IMapper mapper,ContentSafetyClient contentSafetyClient)
    {
        _contentSafetyClient = contentSafetyClient;
        _mapper = mapper;
        _attachmentRepository = attachmentRepository;
        _blobInfrastructure = blobInfrastructure;
    }
    public async Task<Attachments> Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
    {
        var mappedEntity = _mapper.Map<Attachments>(request.AttachmentDto);
        mappedEntity.FileName = $"{mappedEntity.Id}{Path.GetExtension(request.AttachmentDto.file.FileName)}";
        if (await CheckImage(request.AttachmentDto.file))
        {
            await _blobInfrastructure.addBlob(request.AttachmentDto.file,mappedEntity.Id,request.AttachmentDto.FileType);
            var entity = await _attachmentRepository.Add(mappedEntity);
            return entity;
        }

        return null;
    }

    private async Task<bool> CheckImage(IFormFile formFile)
    {
        var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        var image = new ContentSafetyImageData(BinaryData.FromBytes(memoryStream.ToArray()));
        var request = new AnalyzeImageOptions(image);
        var response = await _contentSafetyClient.AnalyzeImageAsync(request);
        if (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Hate)?.Severity > 0 ||
            response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.SelfHarm)?.Severity > 0 ||
            response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Sexual)?.Severity > 0 || 
            response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Violence)?.Severity > 0)
            return false; // Inappropriate content detected
    return true; // Content is safe
    }
}