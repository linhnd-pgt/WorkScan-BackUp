using BaseApp.Constants;
using BaseApp.DTO;
using BaseApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get project list by employee's id
        /// </summary>
        /// <param name="empId">Employee's ID</param>
        /// <returns>Project list by Emp's Id</returns>
        /// <response code="200">Project list by Emp's Id</response>
        /// <response code="401">Employee hasn't logged in yet</response>
        [HttpGet("get-all/{empId}")]
        [ProducesResponseType(typeof(List<ResponseProjectByEmpIdDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProjectListByEmpId([FromRoute] long empId)
        {
            List<ResponseProjectByEmpIdDTO> result = await _projectService.GetAllByEmpId(empId);
            if (result.Count != 0)
            {
                return Ok(new ResponseDataDTO<List<ResponseProjectByEmpIdDTO>>
                {
                    Data = result,
                    Code = StatusCodes.Status200OK,
                    Succeed = true,
                });
            }
            return Ok(new ResponseDataDTO<string>
            {
                Data = DevMessageConstants.OBJECT_IS_EMPTY,
                Code = StatusCodes.Status200OK,
                Succeed = true,
            });
        }

    }
}
