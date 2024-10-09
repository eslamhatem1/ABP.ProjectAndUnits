using ABP.ProjectAndUnits.Aggregates.ProjectAggregate;
using System;
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
        public ProjectManger(IRepository<Project, Guid> repository)
        {
            _repository = repository;
        }
        public async Task<Project> createproject(string name, string projectcode, string descrption, string projectlocation,int numberofunits)
        {
            if (await _repository.AnyAsync(e => e.ProjectCode == projectcode))
            {
                throw new ("thisProjectIsExist");
            }
           
            return new Project(GuidGenerator.Create(), name, projectcode, descrption, projectlocation, numberofunits);

        }


        public async Task<Project> updateProject(Guid id,string name, string projectcode, string descrption, string projectlocation, int numberofunits, Project project)
        {
            
            if (await _repository.AnyAsync(e => e.ProjectCode == projectcode && e.Id != id))
            {
                throw new BusinessException("thisProjectIsExist");
            }


            return project.Update(name, projectcode, descrption, projectlocation, numberofunits);

        }
    }
}
