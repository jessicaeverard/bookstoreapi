/* 
Using automapper allows you to flatten complex object models/
maps objects to different types - this makes code cleaner and faster
*/
using AutoMapper;
using BookStore.Dto;
using BookStore.Entities;
using BookStore.Requests;

namespace BookStore.Automapper
{
    public class bookstoreprofile : Profile
    {
        public bookstoreprofile()
        {
            CreateMap<BookModel, BookDto>();
            CreateMap<BookDto, BookModel>();
            CreateMap<AuthorDto, AuthorModel>();
            CreateMap<UpdateBookRequest, BookModel>();
            CreateMap<AuthorModel, AuthorDto>();
            CreateMap<UpdateAuthorRequest, AuthorModel>();

        }
    }
}
