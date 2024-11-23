using BaseApp.Data;
using BaseApp.Models;

namespace BaseApp.Repository
{
    public interface ITaskRepository : IRepositoryBase<TaskModel>
    {

    }

    public class TaskRepository : RepositoryBase<TaskModel>, ITaskRepository
    {

        public TaskRepository(BaseAppDBContext baseAppDBContext) : base(baseAppDBContext) { }

    }
}
