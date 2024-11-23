using BaseApp.Constants;

namespace BaseApp.DTO
{
    public class ResponseFilterTaskDTO : RequestTaskDTO
    {
        public long Id { get; set; }

        public string? FunctionTitle { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectGithubRepo { get; set; }

    }


}
