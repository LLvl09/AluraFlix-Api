using AluraFlix.Data.Dtos.CategoriaDto;
using AluraFlix.Models;
using AutoMapper;

namespace AluraFlix.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>();
            CreateMap<UpdateCategoriaDto, Categoria>();
        }
    }
}
