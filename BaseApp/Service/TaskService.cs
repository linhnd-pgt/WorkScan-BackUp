using BaseApp.Constants;
using BaseApp.DTO;
using BaseApp.Models;
using BaseApp.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseApp.Service
{

    public interface ITaskService
    {

        public Task<List<string>> GetParentTaskNameList(long taskId);

        public Task<List<ResponseFilterTaskDTO>> FilterTaskList(long empId, DateTime? from, DateTime? to, EnumTypes.TaskStatus? status);
    
        public Task<List<ResponseFunction>> GetFunctionListByProjectId(long empId, long? projectId);

        public Task CreateNewTask(RequestTaskDTO requestTaskDTO);

        public Task UpdateTask(long taskId, RequestTaskDTO requestTaskDTO);
    }

    public class TaskService : ITaskService
    {

        private readonly IRepositoryManager _repositoryManager;

        public TaskService(IRepositoryManager repositoryManager) 
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<List<ResponseFilterTaskDTO>> FilterTaskList(long empId, DateTime? from, DateTime? to, EnumTypes.TaskStatus? status = null)
        {

            var query = _repositoryManager.taskRepository
                .FindByCondition(task => task.EmpId == empId)
                .OrderByDescending(task => task.Id)
                .AsNoTracking();

            if(query != null)
            {
                // Filter by time
                if (from.HasValue && from != DateTime.MinValue)
                {
                    query = query.Where(task => task.StartDate.Date >= from.Value.Date);
                }

                if(to.HasValue && to != DateTime.MinValue)
                {
                    query = query.Where(task => task.EndDate <= to.Value.Date);
                }

                // Filter by status
                if(status.HasValue && Enum.IsDefined(typeof(EnumTypes.TaskStatus), status))
                {
                    query = query.Where(task => task.Status == status.Value);
                }

                return await query.Include(task => task.Project).Select(t => new ResponseFilterTaskDTO
                { 
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Type = t.Type,
                    Status = t.Status,
                    ProjectId =  t.Project.Id,
                    ProjectTitle = t.Project.Name,
                    ProjectGithubRepo = t.Project.GitHubRepo,
                    EmpId = t.EmpId,
                    FunctionId = t.ParentTaskId == null ? null : (long)t.ParentTaskId,
                    FunctionTitle = t.ParentTaskId == null ? null : t.ParentTask.Title,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Estimate = t.Estimate,
                    Note =  t.Note == null ? null : t.Note
                }).AsNoTracking().ToListAsync();

            }

            return new List<ResponseFilterTaskDTO>();

        }

        public async Task<List<string>> GetParentTaskNameList(long taskId)
        {
            List<TaskModel> taskModels = await _repositoryManager.taskRepository
                                        .FindByCondition(task => task.ParentTaskId == null).AsNoTracking().ToListAsync();

            if(taskModels != null)
            {
                return taskModels.Select(task => task.Title).ToList();
            }

            return new List<string>();
        }

        public async Task<List<ResponseFunction>> GetFunctionListByProjectId(long empId, long? projectId)
        {
            var query =  _repositoryManager.taskRepository
                            .FindByCondition(task => task.EmpId == empId).AsNoTracking();

            if(query != null)
            {
                if (projectId.HasValue)
                {
                    query = query.Where(task => task.ProjectId == projectId && task.ParentTaskId != null);
                }

                return await query.Select(task => new ResponseFunction
                {   
                    FunctionId = task.Id,
                    FunctionTitle = task.Title,
                }).ToListAsync();
            }

            return new List<ResponseFunction>();
        }
        public async Task CreateNewTask(RequestTaskDTO requestTaskDTO)
        {
            ProjectModel projectModel = new ProjectModel();

            TaskModel newRequest = new TaskModel
            {
                Title = requestTaskDTO.Title,
                EmpId = requestTaskDTO.EmpId,
                ProjectId = requestTaskDTO.ProjectId,
                Description = requestTaskDTO.Description,
                Type = requestTaskDTO.Type,
                Status = requestTaskDTO.Status,
                StartDate = requestTaskDTO.StartDate,
                ParentTaskId = requestTaskDTO.FunctionId == null ? null : requestTaskDTO.FunctionId,
                EndDate = requestTaskDTO.EndDate,
                Estimate = requestTaskDTO.Estimate,
                Note = String.IsNullOrWhiteSpace(requestTaskDTO.Note) ? "" : requestTaskDTO.Note,
            };
            await _repositoryManager.taskRepository.Create(newRequest);
            await _repositoryManager.SaveAsync();
        }

        public async Task UpdateTask(long taskId, RequestTaskDTO requestTaskDTO)
        {
            TaskModel existedTask = await _repositoryManager.taskRepository.FindByCondition(task => task.Id == taskId).FirstOrDefaultAsync();

            if(existedTask == null)
            {
                throw new Exception(DevMessageConstants.OBJECT_NOT_FOUND);
            }

            existedTask.Title = requestTaskDTO.Title;
            existedTask.Description = requestTaskDTO.Description;
            existedTask.Type = requestTaskDTO.Type;
            existedTask.Status = requestTaskDTO.Status;
            existedTask.ProjectId = requestTaskDTO.ProjectId;   
            existedTask.ParentTaskId = requestTaskDTO.FunctionId == null ? existedTask.ParentTaskId : requestTaskDTO.FunctionId;
            existedTask.StartDate = requestTaskDTO.StartDate;
            existedTask.EndDate = requestTaskDTO.EndDate;
            existedTask.Estimate =  requestTaskDTO.Estimate;
            existedTask.Note = requestTaskDTO.Note == null ? existedTask.Note : requestTaskDTO.Note;
             
            _repositoryManager.taskRepository.Update(existedTask);
            await _repositoryManager.SaveAsync();
        }

    }
}
