using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using ABP.ProjectAndUnits.Base;
using ABP.ProjectAndUnits.DominServices.ProjectServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
namespace ABP.ProjectAndUnits.Projects
{
    public class ProjectAppService : BaseApplicationService, IProjectAppService
    {
        private IRepository<Project, Guid> _repository { get; }
        private ProjectManger _projectManger { get; }
        public ProjectAppService(IRepository<Project, Guid> repository, ProjectManger projectManger)
        {
            _repository = repository;
            _projectManger = projectManger;
        }


        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto input)
        {
            var validationResult = new CreateProjectValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                var execption= GetValidationException(validationResult);
                throw execption;
            }

            var project = await _projectManger.createproject(input.Name, input.ProjectCode, input.Descrption, input.ProjectLocation, input.NumberOfUnits);
            if (input.Units.Count > 0)
            {
                input.Units.ForEach(e => project.AddUnits(GuidGenerator.Create(), e.Descrption, e.Location, e.UnitArea, e.NumberOfRooms, project.Id));
            }
            await _repository.InsertAsync(project, autoSave: true);

            return ObjectMapper.Map<Project, ProjectDto>(project);

        }

        public async Task<string> DeleteProjectAsync(Guid Id)
        {
            var project = await _repository.WithDetailsAsync(e => e.Units).Result.FirstOrDefaultAsync(e => e.Id == Id);
            if (project == null)
            {
                return "هذا العنصر غير موجود";
            }
            if (project.Units.Count() > 0)
                return "لا يمكن حذف المشروع لانه مرتبط بوحدات";

            await _repository.DeleteAsync(project);
            return "تم الحذف بنجاح";

        }

        public async Task<PagedResultDto<ProjectDto>> GetAllProjectsAsync(GetProjectListDto listDto)
        {
            var query = await _repository.WithDetailsAsync(e => e.Units);

            query = query
                .WhereIf(!listDto.Filter.IsNullOrWhiteSpace(), p => p.Name.Contains(listDto.Filter) || p.ProjectCode.Contains(listDto.Filter));

            query = !string.IsNullOrWhiteSpace(listDto.Sorting)
                ? query.OrderBy(listDto.Sorting)
                : query.OrderBy(p => p.Name);

            var projects = await query
                .Skip(listDto.SkipCount)
                .Take(listDto.MaxResultCount)
                .ToListAsync();


            var totalcount = listDto.Filter == null ? await _repository.CountAsync() : await _repository.CountAsync(p => p.Name.Contains(listDto.Filter));
            return new PagedResultDto<ProjectDto>(totalcount, ObjectMapper.Map<List<Project>, List<ProjectDto>>(projects));
        }

        public async Task<ProjectDto> UpdateProjectAsync(UpdateProjectDto dto)
        {
            var validationResult = new UpdateProjectValidator().Validate(dto);

            if (!validationResult.IsValid)
            {
                var execption = GetValidationException(validationResult);
                throw execption;
            }


            var project = await _repository.GetAsync(dto.Id);
            if (project == null)
            {
                throw new Exception("هذا العنصر غير موجود");
            }
            var updateproject = await _projectManger.updateProject(dto.Id, dto.Name, dto.ProjectCode, dto.Descrption, dto.ProjectLocation, dto.NumberOfUnits, project);
            if (dto.Units.Count > 0)
            {

                var unitsToRemove = project.Units.Where(u => !dto.Units.Any(updatedUnit => updatedUnit.Id == u.Id)).ToList();
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
                    var updatedUnit = dto.Units.SingleOrDefault(u => u.Id == existingUnit.Id);
                    if (updatedUnit != null)
                    {
                        existingUnit.Update(updatedUnit.Descrption, updatedUnit.Location, updatedUnit.UnitArea, updatedUnit.NumberOfRooms);
                    }
                }


            }
            await _repository.UpdateAsync(project, autoSave: true);

            return ObjectMapper.Map<Project, ProjectDto>(project);

        }
    }
}
