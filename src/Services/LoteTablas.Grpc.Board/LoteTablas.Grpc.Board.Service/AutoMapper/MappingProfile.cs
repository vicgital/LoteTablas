using AutoMapper;
using LoteTablas.Grpc.Board.Application.Features.BoardDocument.DTO;
using LoteTablas.Grpc.Definitions;

namespace LoteTablas.Grpc.Board.Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BoardDocumentCardModel, BoardDocumentCardDto>().ReverseMap();
            CreateMap<BoardDocumentsRequest, BoardDocumentRequestDto>().ReverseMap();
        }
    }
}
