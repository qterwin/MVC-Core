using AutoMapper;
using mvccore.dto.Book;
using mvccore.dto.LogIn;
using mvccore.Models.Book;
using mvccore.Models.CentralAccess;

namespace mvccore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<Users, LoginDTO>();
            CreateMap<Book, bookDTO>();
            CreateMap<BookUpdateDTO, Book>();
            CreateMap<Book, BookUpdateDTO>();
        }
    }
}   
