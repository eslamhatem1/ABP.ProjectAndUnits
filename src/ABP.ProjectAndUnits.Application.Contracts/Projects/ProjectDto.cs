using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ABP.ProjectAndUnits.Projects
{
    public class ProjectDto:FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string ProjectCode { get; set; }
        public string Descrption { get; set; }
        public string ProjectLocation { get; set; }
        public int NumberOfUnits { get; set; }
    }
}
