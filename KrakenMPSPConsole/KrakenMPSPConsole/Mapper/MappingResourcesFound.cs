using AutoMapper;

using KrakenMPSPConsole.Models;
using KrakenMPSPCrawler.Models;

namespace KrakenMPSPConsole.Mapper
{
    public class MappingResourcesFound : Profile
    {
        public MappingResourcesFound()
        {
            CreateMap<ArispCrawlerModel, ResourcesFound>()
                .ForMember(result => result.Arisp, 
                arisp => arisp.MapFrom(source => source));
            CreateMap<ArpenspCrawlerModel, ResourcesFound>()
                .ForMember(result => result.Arpensp,
                arpensp => arpensp.MapFrom(source => source));
            CreateMap<SielCrawlerModel, ResourcesFound>()
                .ForMember(result => result.Siel, 
                siel => siel.MapFrom(source => source));
            CreateMap<SivecCrawlerModel, ResourcesFound>()
                .ForMember(result => result.Sivec,
                sivec => sivec.MapFrom(source => source));
        }
    }
}
