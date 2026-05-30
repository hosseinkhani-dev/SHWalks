using AutoMapper;
using SHWalks.Application.Walks.DTOs;
using SHWalks.Domain;

namespace SHWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Walk, AddWalkDto>();
        }
    }
}
