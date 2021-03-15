using AutoMapper;
using bookshelf.DTO.Book.BookLogged;
using bookshelf.DTO.Book.Books;
using bookshelf.DTO.Create;
using bookshelf.Model.Books;

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
            this.CreateMap<Model.Books.Author, AuthorLoggedDTO>().ReverseMap();
            this.CreateMap<Model.Books.BookHistory, BookHistoryLoggedDTO>().ReverseMap();
            this.CreateMap<Model.Books.Book, BookLoggedDTO>().ReverseMap();
            this.CreateMap<Model.Books.Review, ReviewLoggedDTO>().ReverseMap();
            this.CreateMap<Model.Books.UserBook, UserBookDTO>().ReverseMap();
            this.CreateMap<Model.Users.User, UserLoggedDTO>().ReverseMap();
            this.CreateMap<ReviewAddDTO, Review>().ReverseMap();
        }
    }
}