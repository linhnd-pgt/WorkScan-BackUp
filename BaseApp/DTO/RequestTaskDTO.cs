using BaseApp.Constants;

namespace BaseApp.DTO
{
    public class RequestTaskDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public EnumTypes.TaskType Type { get; set; }

        public EnumTypes.TaskStatus Status { get; set; }

        public long EmpId { get; set; }

        public long ProjectId { get; set; }

        public long? FunctionId { get; set; }    

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Estimate { get; set; }

        public string? Note { get; set; }
    }
}
