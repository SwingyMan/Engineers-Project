using Application.Authorization.Handlers;
using Application.Authorization.Requirements;
using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Application.Services;
using Autofac.Core;
using Domain.Entities;
using Infrastructure.SignalR;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationService
{
    public static void AddApplicationService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationService).Assembly));
        //user
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

        //role
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<RoleDTO,Role>,Role>),
            typeof(GenericAddCommandHandler<RoleDTO,Role>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<Role>, IEnumerable<Role>>),
            typeof(GenericGetAllQueryHandler<Role>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<Role>, Role>),
            typeof(GenericGetByIdQueryHandler<Role>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<RoleDTO, Role>, Role>),
            typeof(GenericUpdateCommandHandler<RoleDTO, Role>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<Role>>),
            typeof(GenericDeleteCommandHandler<Role>));
        //message
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<MessageDTO,Message>,Message>),
            typeof(GenericAddCommandHandler<MessageDTO,Message>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<Message>, IEnumerable<Message>>),
            typeof(GenericGetAllQueryHandler<Message>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<Message>, Message>),
            typeof(GenericGetByIdQueryHandler<Message>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<MessageDTO, Message>, Message>),
            typeof(GenericUpdateCommandHandler<MessageDTO, Message>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<Message>>),
            typeof(GenericDeleteCommandHandler<Message>));
        //groupuser
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<GroupUserDTO,GroupUser>,GroupUser>),
            typeof(GenericAddCommandHandler<GroupUserDTO,GroupUser>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<GroupUser>, IEnumerable<GroupUser>>),
            typeof(GenericGetAllQueryHandler<GroupUser>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<GroupUser>, GroupUser>),
            typeof(GenericGetByIdQueryHandler<GroupUser>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<GroupUserDTO, GroupUser>, GroupUser>),
            typeof(GenericUpdateCommandHandler<GroupUserDTO, GroupUser>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<GroupUser>>),
            typeof(GenericDeleteCommandHandler<GroupUser>));
        //grouppost
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<GroupPostDTO,GroupPost>,GroupPost>),
            typeof(GenericAddCommandHandler<GroupPostDTO,GroupPost>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<GroupPost>, IEnumerable<GroupPost>>),
            typeof(GenericGetAllQueryHandler<GroupPost>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<GroupPost>, GroupPost>),
            typeof(GenericGetByIdQueryHandler<GroupPost>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<GroupPostDTO, GroupPost>, GroupPost>),
            typeof(GenericUpdateCommandHandler<GroupPostDTO, GroupPost>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<GroupPost>>),
            typeof(GenericDeleteCommandHandler<GroupPost>));
        //group
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<GroupDTO,Group>,Group>),
            typeof(GenericAddCommandHandler<GroupDTO,Group>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetAllQuery<Group>, IEnumerable<Group>>),
            typeof(GenericGetAllQueryHandler<Group>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<Group>, Group>),
            typeof(GenericGetByIdQueryHandler<Group>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<GroupDTO, Group>, Group>),
            typeof(GenericUpdateCommandHandler<GroupDTO, Group>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<Group>>),
            typeof(GenericDeleteCommandHandler<Group>));
        //post
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
        //comment
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericAddCommand<CommentDTO,Comment>,Comment>),
            typeof(GenericAddCommandHandler<CommentDTO,Comment>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericUpdateCommand<CommentDTO, Comment>, Comment>),
            typeof(GenericUpdateCommandHandler<CommentDTO, Comment>));
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericDeleteCommand<Comment>>),
            typeof(GenericDeleteCommandHandler<Comment>));
        //chat
        serviceCollection.AddTransient(typeof(IRequestHandler<GenericGetByIdQuery<Chat>, Chat>),
            typeof(GetChatByIdQueryHandler));

        serviceCollection.AddTransient(typeof(IRequestHandler<CreateChatCommand, Chat>),
            typeof(CreateChatCommandHandler));

        //serviceCollection.AddScoped<IAuthorizationHandler, ChatMemberHandler>();
        serviceCollection.AddAuthorization(options =>
        {
            options.AddPolicy("ChatMemberOrAdmin", policy =>
            {
                policy.Requirements.Add(new ChatMemberRequirement());
            });
            options.AddPolicy("ChatMessageMemberOrAdmin", policy =>
            {
                policy.Requirements.Add(new ChatMessageMemberRequirement());
            });
        });

        serviceCollection.AddTransient<IUserAccessor, UserAccessor>();
    }
}