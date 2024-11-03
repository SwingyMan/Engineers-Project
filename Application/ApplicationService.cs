using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationService
{
    public static void AddApplicationService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationService).Assembly));

        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<User>, IEnumerable<User>>),
            typeof(GenericGetAllQueryHandler<User>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<User>, User>),
            typeof(GenericGetByIdQueryHandler<User>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<UserDTO, User>, User>),
            typeof(GenericUpdateCommandHandler<UserDTO, User>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<User>>),
            typeof(GenericDeleteCommandHandler<User>));

        //serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<PostDTO, Post>, Post>),
        //    typeof(GenericAddCommandHandler<PostDTO, Post>));

        serviceCollection.AddTransient(typeof(IRequestHandler<AddPostCommand, Post>),
            typeof(AddPostCommandHandler));

        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<Post>, IEnumerable<Post>>),
            typeof(GenericGetAllQueryHandler<Post>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<Post>, Post>),
            typeof(GenericGetByIdQueryHandler<Post>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<PostDTO, Post>, Post>),
            typeof(GenericUpdateCommandHandler<PostDTO, Post>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<Post>>),
            typeof(GenericDeleteCommandHandler<Post>));
    }
}