using AutoMapper;
using bookshelf.DTO.Book.Books;

namespace bookshelf.DTO.Book
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            this.CreateMap<Model.Books.Book, BookDTO>()
                .ReverseMap();
            this.CreateMap<Model.Books.Review, ReviewDTO>()
                .ReverseMap();
            this.CreateMap<Model.Books.BookISBN, BookISBNDTO>().ReverseMap();
            this.CreateMap<Model.Users.User, UserDTO>().ReverseMap();
        }
    }
}