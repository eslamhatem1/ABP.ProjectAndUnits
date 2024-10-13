using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using ABP.ProjectAndUnits.Projects;
using ABP.ProjectAndUnits.Units;
using AutoMapper;
using System.Collections.Generic;

namespace ABP.ProjectAndUnits;

public class ProjectAndUnitsApplicationAutoMapperProfile : Profile
{
    public ProjectAndUnitsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CreateProjectDto, Project>()
             .ForMember(dest => dest.Units, opt => opt.MapFrom(src => src.Units))
             .ReverseMap();
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<CreateUnitDto, Project>().ReverseMap();
        //CreateMap<List<CreateUnitDto>, List<Unit>>().ReverseMap();
        CreateMap<CreateUnitDto, Unit>().ReverseMap();

    }
}
