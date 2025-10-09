
using AutoMapper;
using LoteTablas.Core.Models;
using LoteTablas.Core.Service.Definition;

namespace LoteTablas.Core.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? string.Empty : s);
            CreateMap<int?, int>().ConvertUsing(s => s ?? 0);

            CreateMap<Models.DAO.Lottery, Models.Lottery>().ReverseMap();
            CreateMap<Models.DAO.LotteryCard, Models.LotteryCard>().ReverseMap();
            CreateMap<Models.DAO.LotteryType, Models.LotteryType>().ReverseMap();
            CreateMap<Models.DAO.Card, Models.Card>().ReverseMap();
            CreateMap<Models.DAO.BoardSize, Models.BoardSize>().ReverseMap();
            CreateMap<Models.DAO.BoardCard, Models.BoardCard>().ReverseMap();
            CreateMap<Models.DAO.Board, Models.Board>().ReverseMap();
            //CreateMap<Models.DAO.BoardTemplate, Models.BoardTemplate>().ReverseMap();

            CreateMap<Lottery, LotteryModel>().ReverseMap();
            CreateMap<LotteryCard, LotteryCardModel>().ReverseMap();
            CreateMap<LotteryType, LotteryTypeModel>().ReverseMap();
            CreateMap<Card, CardModel>().ReverseMap();


            CreateMap<SaveBoardRequest, Models.Board>();
            CreateMap<BoardSize, BoardSizeModel>().ReverseMap();
            CreateMap<Board, BoardModel>().ReverseMap();
            CreateMap<BoardCard, BoardCardModel>().ReverseMap();
            CreateMap<BoardLite, BoardLiteModel>().ReverseMap();
            CreateMap<BoardCardLite, BoardCardLiteModel>().ReverseMap();

        }
    }
}
