using ABP.ProjectAndUnits.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.ProjectAndUnits.Projects
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectValidator(IStringLocalizer<ProjectAndUnitsResource> localizer)
        {
            RuleFor(command => command.Id)
          .NotEmpty().WithMessage(localizer["item:required"]);

            RuleFor(command => command.ProjectCode)
           .NotEmpty().WithMessage(localizer["item:required"])
           .Length(5).WithMessage(localizer["ProjectMustConatin5Characters"]);


            RuleFor(command => command.Name)
                .NotEmpty().WithMessage(localizer["item:required"])
                .Length(2, 100).WithMessage(localizer["ProjectNameMustBetween2And100"]);

            RuleFor(command => command.Descrption)
                .MaximumLength(500).WithMessage(localizer["DescrptionNotExceed500"]);



            RuleFor(command => command.NumberOfUnits)
             .Must(count => count > 0 && count <= 100000)
             .WithMessage("The number of units must be greater than zero and not exceed 100,000.");


            RuleForEach(x => x.Units).ChildRules(unit =>
            {
                unit.RuleFor(u => u.Descrption)
               .NotEmpty().WithMessage(localizer["item:required"]);

                unit.RuleFor(u => u.UnitArea)
                    .GreaterThan(0).WithMessage(localizer["ItemGreaterThanzero"]);

                unit.RuleFor(u => u.NumberOfRooms)
                    .GreaterThan(0).WithMessage(localizer["ItemGreaterThanzero"]);
            });


        }
    }
}
