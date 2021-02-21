using AutoMapper;

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
        }
    }
}