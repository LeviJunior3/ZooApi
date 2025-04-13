namespace ZooApi.Profile
{
    using AutoMapper;
    using ZooApi.Dtos;
    using ZooApi.Models;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Animal, AnimalDto>()
                .ForMember(dest => dest.CuidadoIds,
                           opt => opt.MapFrom(src => src.AnimaisCuidados.Select(ac => ac.CuidadoId)))
                .ReverseMap()
                .ForMember(dest => dest.AnimaisCuidados,
                           opt => opt.MapFrom(src => src.CuidadoIds.Select(id => new AnimalCuidado { CuidadoId = id })));

            CreateMap<Cuidado, CuidadoDto>().ReverseMap();
        }
    }
}
