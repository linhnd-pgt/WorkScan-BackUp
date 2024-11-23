using BaseApp.Data;
using BaseApp.Models;

namespace BaseApp.Repository
{
    public interface IProjectRepostitory : IRepositoryBase<ProjectModel>
    {

    }

    public class ProjectRepostitory : RepositoryBase<ProjectModel>, IProjectRepostitory
    {

        public ProjectRepostitory(BaseAppDBContext baseAppDBContext) : base(baseAppDBContext) { }

    }
}
