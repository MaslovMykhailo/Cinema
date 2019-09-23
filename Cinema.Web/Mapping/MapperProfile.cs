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
            CreateMap<TicketModel, Ticket>();
            CreateMap<SessionModel, Session>();
            CreateMap<VisitorModel, Visitor>();
            CreateMap<PlaceModel, Place>();
        }
    }

}
