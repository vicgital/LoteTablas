using AutoMapper;
using LoteTablas.Grpc.Definitions;
using LoteTablas.Grpc.Lottery.Application.Features.Lottery.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryCard.DTO;
using LoteTablas.Grpc.Lottery.Application.Features.LotteryType.DTO;

namespace LoteTablas.Grpc.Lottery.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? string.Empty : s);
            CreateMap<int?, int>().ConvertUsing(s => s ?? 0);

            CreateMap<LotteryDto, LotteryModel>().ReverseMap();
            CreateMap<LotteryCardDto, LotteryCardModel>().ReverseMap();
            CreateMap<LotteryTypeDto, LotteryTypeModel>().ReverseMap();

        }
    }
}
