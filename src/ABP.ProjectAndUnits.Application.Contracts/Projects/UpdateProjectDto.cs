using ABP.ProjectAndUnits.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.ProjectAndUnits.Projects
{
    public class UpdateProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProjectCode { get; set; }
        public string Descrption { get; set; }
        public string ProjectLocation { get; set; }
        public int NumberOfUnits { get; set; }
        public List<UpdateUnitDto> Units { get; set; } = new List<UpdateUnitDto>();
    }
}
