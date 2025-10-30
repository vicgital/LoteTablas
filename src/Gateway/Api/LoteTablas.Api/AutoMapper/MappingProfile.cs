using AutoMapper;

namespace LoteTablas.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, string>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? string.Empty : s);
            CreateMap<int?, int>().ConvertUsing(s => s ?? 0);
        }

    }
}
