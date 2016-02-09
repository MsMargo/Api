using AutoMapper;
using RestApi.Models.DataModel;
using RestApi.Models.ViewModel;

namespace RestApi.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Post, PostView>();
            Mapper.CreateMap<Comment, CommentView>();

            Mapper.CreateMap<User, UserView>();
            Mapper.CreateMap<Address, AddressView>();
            Mapper.CreateMap<Geo, GeoView>();
            Mapper.CreateMap<Company, CompanyView>();
        }
    }
}