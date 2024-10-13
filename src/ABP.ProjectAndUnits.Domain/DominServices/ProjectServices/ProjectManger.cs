using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using ABP.ProjectAndUnits.Localization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace ABP.ProjectAndUnits.DominServices.ProjectServices
{
    public class ProjectManger : DomainService
    {
        private readonly IRepository<Project, Guid> _repository;
        private readonly IStringLocalizer<ProjectAndUnitsResource> _localizer;

        public ProjectManger(IRepository<Project, Guid> repository, IStringLocalizer<ProjectAndUnitsResource> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }
        public async Task<Project> createproject(string name, string projectcode, string descrption, string projectlocation,int numberofunits,List<Unit> units =null)
        {
            if (await _repository.AnyAsync(e => e.ProjectCode == projectcode))
            {
                throw new BusinessException("thisProjectIsExist");
            }
            
            var project = new Project(GuidGenerator.Create(), name, projectcode, descrption, projectlocation, numberofunits);
            if (units.Count > 0 && units != null)
            {
                units.ForEach(e => project.AddUnits(GuidGenerator.Create(), e.Descrption, e.Location, e.UnitArea, e.NumberOfRooms, project.Id));
            }

            return project;

        }

        public async Task<Project> DeleteProject(Guid Id)
        {
            var project = _repository.WithDetailsAsync(e => e.Units).Result.FirstOrDefault(e => e.Id == Id);
            if (project == null)
            {
                throw new BusinessException(_localizer["ItemNotFound"]);

            }
            if (project.Units.Count() > 0)
                throw new BusinessException(_localizer["CantDeleteThisItemBecauseToRelatedToUnit"]);

            await _repository.DeleteAsync(project,autoSave:true);

            return project;
        }

        public async Task<Project> updateProject(Guid id,string name, string projectcode, string descrption, string projectlocation, int numberofunits, Project project,List<Unit> units = null)
        {
            
            if (await _repository.AnyAsync(e => e.ProjectCode == projectcode && e.Id != id))
            {
                throw new BusinessException("thisProjectIsExist");
            }

            var Project = await _repository.GetAsync(id);
            if (Project == null)
            {
                throw new BusinessException(_localizer["ItemNotFound"]);
            }
            if (units.Count > 0 && units != null)
            {

                var unitsToRemove = project.Units.Where(u => !units.Any(updatedUnit => updatedUnit.Id == u.Id)).ToList();
                foreach (var unit in unitsToRemove)
                {
                    project.RemoveUnit(unit);
                }

                var unitsToAdd = project.Units.Where(updatedUnit => !project.Units.Any(u => u.Id == updatedUnit.Id)).ToList();
                foreach (var unit in unitsToAdd)
                {
                    project.AddUnit(unit);
                }

                foreach (var existingUnit in project.Units)
                {
                    var updatedUnit = units.SingleOrDefault(u => u.Id == existingUnit.Id);
                    if (updatedUnit != null)
                    {
                        existingUnit.Update(updatedUnit.Descrption, updatedUnit.Location, updatedUnit.UnitArea, updatedUnit.NumberOfRooms);
                    }
                }


            }
            await _repository.UpdateAsync(project, autoSave: true);

            return project.Update(name, projectcode, descrption, projectlocation, numberofunits);

        }
    }
}
