using ABP.ProjectAndUnits.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace ABP.ProjectAndUnits.Projects
{
    public class CreateProjectDto:EntityDto<Guid>
    {

        public string Name { get;  set; }
        public string ProjectCode { get;  set; }
        public string Descrption { get;  set; }
        public string ProjectLocation { get;  set; }
        public int NumberOfUnits { get;  set; }
        public List<CreateUnitDto> Units { get; set; } = new List<CreateUnitDto>();

    }
}
