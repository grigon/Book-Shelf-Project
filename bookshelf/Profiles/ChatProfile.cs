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
            this.CreateMap<ChatMessage, ChatMessageDTO>()
            .ForMember(c => c.Autor, o => o.MapFrom(a => a.MessageAuthor.UserName)).ReverseMap();
        }
    }
}
