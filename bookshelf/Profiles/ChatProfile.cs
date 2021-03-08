using AutoMapper;
using bookshelf.DTOS;
using bookshelf.Model.Chats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookshelf.Profiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            //read
            this.CreateMap<ChatMessage, ChatMessageReadDTO>()
            .ForMember(c => c.Autor, o => o.MapFrom(a => a.MessageAuthor.UserName)).ReverseMap();








            //create
            this.CreateMap<ChatMessage, ChatMessageCreateDTO>()
                .ForMember(c => c.AutorId, o => o.MapFrom(a => a.MessageAuthor.Id))
                .ForMember(c => c.ChatId, o => o.MapFrom(a => a.Chat.ChatId))
                .ReverseMap();

            this.CreateMap<ChatUser, ChatUserCreateDTO>()
                .ForMember(c => c.ChatId, o => o.MapFrom(c => c.Chat.ChatId))
                .ForMember(u => u.UserId, o => o.MapFrom(user => user.User.Id))
                .ReverseMap();

        }
    }
}
