using Application.DTOs;
using AutoMapper;
using Domain.Entities;

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
    }
}