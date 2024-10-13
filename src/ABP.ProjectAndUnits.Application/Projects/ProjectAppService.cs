using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using ABP.ProjectAndUnits.Base;
using ABP.ProjectAndUnits.DominServices.ProjectServices;
using ABP.ProjectAndUnits.Localization;
using ABP.ProjectAndUnits.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
namespace ABP.ProjectAndUnits.Projects
{
    public class ProjectAppService : BaseApplicationService, IProjectAppService
    {
        private IRepository<Project, Guid> _repository { get; }
        private ProjectManger _projectManger { get; }
        private readonly IStringLocalizer<ProjectAndUnitsResource> _localizer;

        public ProjectAppService(IRepository<Project, Guid> repository, ProjectManger projectManger, IStringLocalizer<ProjectAndUnitsResource> localizer)
        {
            _repository = repository;
            _projectManger = projectManger;
            _localizer = localizer;
        }


        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto input)
        {
            var validationResult = new CreateProjectValidator(_localizer).Validate(input);

            if (!validationResult.IsValid)
            {
                var execption= GetValidationException(validationResult);
                throw execption;
            }

            var Units = ObjectMapper.Map<List<CreateUnitDto>, List<Unit>>(input.Units);
            var project = await _projectManger.createproject(input.Name, input.ProjectCode, input.Descrption, input.ProjectLocation, input.NumberOfUnits, Units);

            await _repository.InsertAsync(project, autoSave: true);

            return ObjectMapper.Map<Project, ProjectDto>(project);

        }

        public async Task<string> DeleteProjectAsync(Guid Id)
        {
             await _projectManger.DeleteProject(Id);
             return _localizer["DeletedSuccess"];

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
            var validationResult = new UpdateProjectValidator(_localizer).Validate(dto);
            if (!validationResult.IsValid)
            {
                var execption = GetValidationException(validationResult);
                throw execption;
            }
            var project = await _repository.GetAsync(dto.Id);
            if (project == null)
            {
                throw new UserFriendlyException(_localizer["ItemNotFound"]);
            }
            var Units = ObjectMapper.Map<List<UpdateUnitDto>, List<Unit>>(dto.Units);

            var updateproject = await _projectManger.updateProject(dto.Id, dto.Name, dto.ProjectCode, dto.Descrption, dto.ProjectLocation, dto.NumberOfUnits, project, Units);
           
            return ObjectMapper.Map<Project, ProjectDto>(project);

        }
    }
}
