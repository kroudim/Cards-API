using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Entities.Card, Models.CardDto>();
            CreateMap<Entities.Card, Models.CardForUpdateDto>();
            CreateMap< Models.CardForUpdateDto, Entities.Card>();
            CreateMap<Models.CardForCreationDto, Entities.Card>();
      }
    }
}
