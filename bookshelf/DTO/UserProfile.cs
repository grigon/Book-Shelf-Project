using AutoMapper;
using bookshelf.DTO.Create;
using bookshelf.DTO.Read;
using bookshelf.DTO.Update;

namespace bookshelf.DTO
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Model.Users.User, UserReadDTO>();
            CreateMap<Model.Users.User, UserEditDTO>().ReverseMap();
            CreateMap<Model.Users.User, UserCreateDTO>().ReverseMap();
            CreateMap<UserLoginDTO, Model.Users.User>().ReverseMap();
            // .ForMember(a => a.Password, s => s.Ignore())
            // .ReverseMap();
        }
    }
}