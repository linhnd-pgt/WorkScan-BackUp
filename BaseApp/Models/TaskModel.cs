using BaseApp.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BaseApp.Models
{
    [Table("task")]
    public class TaskModel : BaseModel
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("type")]
        public EnumTypes.TaskType Type { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("estimate")]
        public int Estimate { get; set; }

        [Column("status")]
        public EnumTypes.TaskStatus Status { get; set; }

        [Column("note")]
        public string? Note { get; set; }   

        [Column("project_id")]
        public long ProjectId { get; set; }

        public ProjectModel Project { get; set; }

        [Column("emp_id")]
        public long EmpId {  get; set; }

        [JsonIgnore]
        public EmployeeModel Employee { get; set; }

        [Column("parent_task_id")]
        public long? ParentTaskId { get; set; }

        public TaskModel ParentTask { get; set; }

        [JsonIgnore]
        public ICollection<TaskModel> ChildTaskList { get; set; } = new List<TaskModel>();

    }
}
