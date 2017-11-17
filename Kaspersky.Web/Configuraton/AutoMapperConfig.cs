using AutoMapper;
using Kaspersky.Data.Domain;
using Kaspersky.Web.Models;

namespace Kaspersky.Web.Configuraton
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Author, AutorModel>().ReverseMap();
        }
    }
}
