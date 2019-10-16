using AutoMapper;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;

namespace Cinema.Web.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<FilmModel, Film>();
            CreateMap<HallModel, Hall>();
            CreateMap<TicketModel, Ticket>().ReverseMap();
            CreateMap<SessionModel, Session>();
            CreateMap<VisitorModel, Visitor>();
            CreateMap<PlaceModel, Place>();

            CreateMap<Ticket, FilteredTicket>()
                .ForMember(_ => _.Film, opt => opt.MapFrom(src => src.Film))
                .ForMember(_ => _.PlaceNumber, opt => opt.MapFrom(src => src.Place.Number));
        }
    }

}
