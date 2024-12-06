using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.SignalR;

namespace Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDTO, User>();
        CreateMap<UserLoginDTO, User>();
        CreateMap<UserRegisterDTO, User>();
        CreateMap<PostDTO, Post>();
        CreateMap<MessageDTO, Message>();
        CreateMap<GroupUserDTO, GroupUser>();
        CreateMap<GroupPostDTO, GroupPost>();
        CreateMap<GroupDTO, Group>();
        CreateMap<ChatUserDTO, ChatUser>();
        CreateMap<ChatMessageDTO, ChatMessage>();
        CreateMap<ChatDTO, Chat>();
        CreateMap<ChatHubMessageDTO, Message>();
        CreateMap<AttachmentDTO, Attachments>()
            .ForMember(x=>x.Id,opt=>opt.MapFrom(src=>Guid.NewGuid()))
            .ForMember(x=>x.Type,opt=>opt.MapFrom(src=>src.FileType));

        //CreateMap<Chat, ChatResponseObject>()
        //    .IncludeMembers(src => src.Messages)
        //    .IncludeMembers(src => src.Users);

        CreateMap<Chat, ChatResponseObject>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

        CreateMap<User, ChatUserResponseObject>();
        CreateMap<Message, ChatMessageResponseObject>();
    }
}