using Domain.Entities;
using Infrastructure.Blobs;
using Infrastructure.IRepositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands;

public class AddAvatarCommandHandler : IRequestHandler<AddAvatarCommand, User>
{
    public AddAvatarCommandHandler(SocialPlatformDbContext dbContext, IBlobInfrastructure blobInfrastructure)
    {
        _dbContext = dbContext;
        _blobInfrastructure = blobInfrastructure;
    }
    private readonly SocialPlatformDbContext _dbContext;
    private readonly IBlobInfrastructure _blobInfrastructure;
    public async Task<User> Handle(AddAvatarCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request);
        var user =await _dbContext.Users.FirstAsync(x=>x.Id==request.UserId);
        var guid = Guid.NewGuid();
        user.AvatarFileName = $"{guid}{Path.GetExtension(request.Avatar.FileName)}";
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        _blobInfrastructure.addBlob(request.Avatar, guid, "avatars");
        return user;
    }
}