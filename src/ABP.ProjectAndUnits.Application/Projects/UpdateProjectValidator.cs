using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABP.ProjectAndUnits.Projects
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectValidator()
        {
            RuleFor(command => command.Id)
          .NotEmpty().WithMessage("Id is required.");

            RuleFor(command => command.ProjectCode)
           .NotEmpty().WithMessage("Project code is required.")
           .Length(5).WithMessage("Project code must consist of 5 characters/numbers.")
           .Matches("^[a-zA-Z0-9]{5}$").WithMessage("Project code must consist of letters and numbers only.");


            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(command => command.Descrption)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");



            RuleFor(command => command.NumberOfUnits)
             .Must(count => count > 0 && count <= 100000)
             .WithMessage("The number of units must be greater than zero and not exceed 100,000.");


            RuleForEach(x => x.Units).ChildRules(unit =>
            {
                unit.RuleFor(u => u.Descrption)
               .NotEmpty().WithMessage("Description is required.");

                unit.RuleFor(u => u.UnitArea)
                    .GreaterThan(0).WithMessage("Area must be greater than zero.");

                unit.RuleFor(u => u.NumberOfRooms)
                    .GreaterThan(0).WithMessage("Number of rooms must be greater than zero.");
            });


        }
    }
}
