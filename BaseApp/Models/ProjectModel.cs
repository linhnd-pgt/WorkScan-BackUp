using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseApp.Models
{
    [Table("project")]
    public class ProjectModel : BaseModel
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("github_repo")]
        public string GitHubRepo { get; set; }

        [Column("client_id")]
        public long ClientId { get; set; }

        public ClientModel Client { get; set; }

        public ICollection<TaskModel> TaskList { get; set; }

    }
}
