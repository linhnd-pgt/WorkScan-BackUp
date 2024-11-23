using BaseApp.DTO;
using BaseApp.Models;
using BaseApp.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace BaseApp.Service
{

    public interface IProjectService
    {
        Task<List<ResponseProjectByEmpIdDTO>> GetAllByEmpId(long empId);
    }

    public class ProjectService : IProjectService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ProjectService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager; 
        }

        public async Task<List<ResponseProjectByEmpIdDTO>> GetAllByEmpId(long empId)
        {
            var projectListByEmpId = _repositoryManager.employeeRepository
                                    .FindByCondition(e => e.Id == 1) 
                                    .Include(e => e.TaskList)
                                    .ThenInclude(t => t.Project) 
                                    .SelectMany(e => e.TaskList.Select(t => t.Project)) 
                                    .Distinct() 
                                    .ToList();

            if (projectListByEmpId.Count > 0)
            {
                return projectListByEmpId.Select(project => new ResponseProjectByEmpIdDTO
                {
                    Id = project.Id,
                    Name = project.Name,
                    GitHubRepo = project.GitHubRepo
                }).ToList();
            }

            return new List<ResponseProjectByEmpIdDTO>();
        }
    }
}
