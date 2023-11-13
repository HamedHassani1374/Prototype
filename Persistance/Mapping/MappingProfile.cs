using AutoMapper;
using Prototype.Model.DTOs.Response.UserAuth;
using Prototype.Model.Models;

namespace Prototype.Persistance.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User , UserLoginResponseDTO>();
        } 
    }
}
