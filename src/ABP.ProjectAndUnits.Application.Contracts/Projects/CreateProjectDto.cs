using ABP.ProjectAndUnits.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace ABP.ProjectAndUnits.Projects
{
    public class CreateProjectDto:EntityDto<Guid>
    {
        [Required(ErrorMessage = "Name is required.")]

        public string Name { get;  set; }

        [Required(ErrorMessage = "Project code is required.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Project code must consist of 5 characters/numbers.")]
        [RegularExpression("^[a-zA-Z0-9]{5}$", ErrorMessage = "Project code must consist of letters and numbers only.")]
        public string ProjectCode { get;  set; }
        [Required(ErrorMessage = "Descrption is required.")]
        public string Descrption { get;  set; }
        public string ProjectLocation { get;  set; }

        [Range(1, 100000, ErrorMessage = "The number of units must be greater than zero and not exceed 100,000.")]

        public int NumberOfUnits { get;  set; }
        public List<CreateUnitDto> Units { get; set; } = new List<CreateUnitDto>();

    }
}
