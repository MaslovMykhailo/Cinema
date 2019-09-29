using AutoMapper;
using Cinema.Persisted.Entities;
using Cinema.Web.Models;

namespace Cinema.Web.Mapping
{
    public class MapperProfile : Profile
    {
        private static MapperProfile instance;
        private MapperProfile()
        {
            CreateMap<FilmModel, Film>();
            CreateMap<HallModel, Hall>();
            CreateMap<TicketModel, Ticket>();
            CreateMap<SessionModel, Session>();
            CreateMap<VisitorModel, Visitor>();
            CreateMap<PlaceModel, Place>();
        }

        public static MapperProfile getInstance() {
            if (instance == null) {
                instance = new MapperProfile();
            }
            return instance;
        }
    }

}
