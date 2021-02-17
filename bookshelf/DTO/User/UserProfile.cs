using AutoMapper;

namespace bookshelf.DTO.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<Model.Users.User, UserModel>()
                .ForMember(a => a.Password, s => s.Ignore())
                .ReverseMap();


        }
    }
}