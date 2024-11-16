﻿using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Serilog;

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
        CreateMap<User,UserReturnDTO>().ForMember(x=>x.Id,opt=>opt.MapFrom(src=>src.Id))
            .ForMember(x=>x.FirstName,opt=>opt.MapFrom(src=>src.Username))
            .ForMember(x=>x.AvatarName,opt=>opt.MapFrom(src=>src.AvatarFileName));
        CreateMap<AttachmentDTO, Attachments>()
            .ForMember(x=>x.Id,opt=>opt.MapFrom(src=>Guid.NewGuid()))
            .ForMember(x=>x.Type,opt=>opt.MapFrom(src=>src.FileType));
    }
}