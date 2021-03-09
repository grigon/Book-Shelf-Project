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

            MapUpdateDtos();
            MapCreateDtos();
            MapReadDtos();
            //read
            //this.CreateMap<ChatMessage, ChatMessageReadDTO>()
            //.ForMember(c => c.Autor, o => o.MapFrom(a => a.MessageAuthor.UserName)).ReverseMap();



            //this.CreateMap<ChatUser, ChatUserReadDTO>()
            //    .ForMember(c => c.userName, o => o.MapFrom(a => a.User.UserName))
            //    .ForMember(id => id.userId, o => o.MapFrom(a => a.User.Id))
            //    .ReverseMap();



            //create
            //this.CreateMap<ChatMessage, ChatMessageCreateDTO>()
            //    .ForMember(c => c.AutorId, o => o.MapFrom(a => a.MessageAuthor.Id))
            //    .ForMember(c => c.ChatId, o => o.MapFrom(a => a.Chat.ChatId))
            //    .ReverseMap();

            //this.CreateMap<ChatUser, ChatUserCreateDTO>()
            //    .ForMember(c => c.chatId, o => o.MapFrom(c => c.Chat.ChatId))
            //    .ForMember(u => u.userId, o => o.MapFrom(user => user.User.Id))
            //    .ReverseMap();


            // update
        }

        private void MapUpdateDtos()
        {
            this.CreateMap<ChatMessage, ChatMessageUpdateDTO>().ReverseMap();
        }

        private void MapCreateDtos()
        {
            this.CreateMap<ChatMessage, ChatMessageCreateDTO>()
                .ForMember(c => c.AutorId, o => o.MapFrom(a => a.MessageAuthor.Id))
                .ForMember(c => c.ChatId, o => o.MapFrom(a => a.Chat.ChatId))
                .ReverseMap();

            this.CreateMap<ChatUser, ChatUserCreateDTO>()
                .ForMember(c => c.chatId, o => o.MapFrom(c => c.Chat.ChatId))
                .ForMember(u => u.userId, o => o.MapFrom(user => user.User.Id))
                .ReverseMap();
        }

        private void MapReadDtos()
        {
            this.CreateMap<ChatMessage, ChatMessageReadDTO>()
            .ForMember(c => c.Autor, o => o.MapFrom(a => a.MessageAuthor.UserName)).ReverseMap();


            this.CreateMap<ChatUser, ChatUserReadDTO>()
                .ForMember(c => c.userName, o => o.MapFrom(a => a.User.UserName))
                .ForMember(id => id.userId, o => o.MapFrom(a => a.User.Id))
                .ReverseMap();
        }
    }
}
