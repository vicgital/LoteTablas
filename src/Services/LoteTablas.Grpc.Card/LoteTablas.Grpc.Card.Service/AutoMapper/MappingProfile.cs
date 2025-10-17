using AutoMapper;
using LoteTablas.Grpc.Card.Application.Features.Card.DTO;
using LoteTablas.Grpc.Definitions;

namespace LoteTablas.Grpc.Card.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? string.Empty : s);
            CreateMap<int?, int>().ConvertUsing(s => s ?? 0);
            CreateMap<CardDto, CardModel>().ReverseMap();
        }
    }
}
