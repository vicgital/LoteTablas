using AutoMapper;
using LoteTablas.Api.Models;
using LoteTablas.Api.Models.MVP;

namespace LoteTablas.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? string.Empty : s);
            CreateMap<int?, int>().ConvertUsing(s => s ?? 0);

            CreateMap<Core.Service.Definition.CardModel, Card>().ReverseMap();

            CreateMap<Core.Service.Definition.LotteryModel, Lottery>().ReverseMap();
            CreateMap<Core.Service.Definition.LotteryCardModel, LotteryCard>().ReverseMap();
            CreateMap<Core.Service.Definition.LotteryTypeModel, LotteryType>().ReverseMap();

            CreateMap<Core.Service.Definition.BoardLiteModel, Models.MVP.Board>().ReverseMap();
            CreateMap<Core.Service.Definition.BoardCardLiteModel, Models.MVP.BoardCard>().ReverseMap();



        }

    }
}
