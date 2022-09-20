using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Pokemon, PokemonDto>().ReverseMap(); ;
            CreateMap<Category, CategoryDto>().ReverseMap(); ;
            CreateMap<Country, CountryDto>().ReverseMap(); ;
            CreateMap<Owner, OwnerDto>().ReverseMap(); ;
            CreateMap<Review, ReviewDto>().ReverseMap(); ;
            CreateMap<Reviewer, ReviewerDto>().ReverseMap(); ;
        }
    }
}
