using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using ABP.ProjectAndUnits.Projects;
using ABP.ProjectAndUnits.Units;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.ProjectAndUnits.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
              CreateMap<CreateProjectDto, Project>()
                .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units))
                .ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<CreateUnitDto, Project>().ReverseMap();

        }
    }
}
