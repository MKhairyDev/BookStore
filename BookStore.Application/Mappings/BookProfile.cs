using AutoMapper;
using BookStore.Application.Commands.Books.CreateBookCommand;
using BookStore.Application.Commands.Books.UpdateBookCommand;
using BookStore.Application.Dto;
using BookStore.Application.Parameters;
using BookStore.Domain.Entities;

namespace BookStore.Application.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookCommand, Book>().ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.Authors, option => option.Ignore());
            CreateMap<CreateBookCommand, Book>().ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.Authors, option => option.Ignore());
            CreateMap<UpdateBookCommand, Book>().ForMember(dest => dest.Id, option => option.Ignore())
                .ForMember(dest => dest.Authors, option => option.Ignore());
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorDto>();
            CreateMap<PagedList<Book>,PagedList<BookDto>>();

        }
    }
}
