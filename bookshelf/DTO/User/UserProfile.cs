using AutoMapper;

namespace bookshelf.DTO.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<Model.Users.User, UserModel>();

        }
    }
}