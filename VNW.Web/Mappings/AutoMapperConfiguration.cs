using AutoMapper;
using VNW.Data.Models;
using VNW.Web.Models;

namespace VNW.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<NganhNghe, NganhNgheViewModel>();
            Mapper.CreateMap<Tinh, TinhViewModel>();
        }
    }
}