using AutoMapper;
using Scalable_Web.DTO.Response;
using Scalable_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scalable_Web.Profiles
{
    public class ReturnDifferenceReponse : Profile
    {
        public ReturnDifferenceReponse()
        {
            CreateMap<Difference, DifferenceResponseDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.Left)).ReverseMap();
        }
    }
}
