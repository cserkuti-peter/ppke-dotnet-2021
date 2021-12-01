using AutoMapper;
using RecipeBookApi.Dtos;
using RecipeBookApi.Models;
using RecipeBookApi.ViewModels;

namespace RecipeBookApi.Profiles
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<NewRecipeDto, Recipe>();
            CreateMap<UpdateRecipeDto, Recipe>();
            CreateMap<Recipe, RecipeRowVM>()
                .ForMember(
                    dest => dest.RecipeBookName,
                    opt => opt.MapFrom(src => src.RecipeBook.Name));
            CreateMap<Recipe, RecipeVM>()
                .ForMember(
                    dest => dest.CookTime,
                    opt => opt.MapFrom(src => src.CookTimeMinutes))
                .ForMember(
                    dest => dest.Rating,
                    opt => opt.MapFrom(src => src.RatingsAvg));

        }
    }
}
