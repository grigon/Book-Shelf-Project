using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using bookshelf.DTOS;
using bookshelf.Model.Chats;

namespace bookshelf.Profiles
{
    public class ChatProfiles : Profile
    {
        public ChatProfiles()
        {
            this.CreateMap<ChatMessage, ChatMessageDTO>()
                .ForMember(c => c.Autor, o => o.MapFrom(a => a.MessageAuthor.UserName)).ReverseMap();
        }
        
    }
}
