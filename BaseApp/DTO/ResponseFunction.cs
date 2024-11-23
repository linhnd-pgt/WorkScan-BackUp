using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApp.DTO
{
    public class ResponseFunction
    {
        public long FunctionId { get; set; }

        public string FunctionTitle { get; set; }
    }
}
