using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Serilog;
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
        CreateMap<CommentDTO, Comment>()
            .ForMember(x=>x.CreatedDate,y=>y.MapFrom(src=>DateTime.Now));
        CreateMap<User,UserReturnDTO>().ForMember(x=>x.Id,opt=>opt.MapFrom(src=>src.Id))
            .ForMember(x=>x.Username,opt=>opt.MapFrom(src=>src.Username))
            .ForMember(x=>x.AvatarFileName,opt=>opt.MapFrom(src=>src.AvatarFileName));
        CreateMap<AttachmentDTO, Attachments>()
            .ForMember(x=>x.Id,opt=>opt.MapFrom(src=>Guid.NewGuid()));

        CreateMap<Chat, ChatResponseObject>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

        CreateMap<User, ChatUserResponseObject>();
        CreateMap<Message, ChatMessageResponseObject>();
    }
}