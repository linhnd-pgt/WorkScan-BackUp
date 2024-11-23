using BaseApp.Constants;
using BaseApp.DTO;
using BaseApp.Models;
using BaseApp.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get task list by employee's id
        /// </summary>
        /// <param name="empId">Employee's ID</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="status"></param>
        /// <returns>Task list by Emp's Id</returns>
        /// <response code="200">Task list by Emp's Id</response>
        /// <response code="401">Employee hasn't logged in yet</response>
        [HttpGet("get-all/{empId}")]
        [ProducesResponseType(typeof(ResponseDataDTO<List<ResponseFilterTaskDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTaskListByEmpId([FromRoute] long empId, [FromQuery] DateTime? from, DateTime? to, EnumTypes.TaskStatus? status)
        {

            List<ResponseFilterTaskDTO> result = await _taskService.FilterTaskList(empId, from, to, status);
            if (result.Count != 0)
            {
                return Ok(new ResponseDataDTO<List<ResponseFilterTaskDTO>>
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

        /// <summary>
        /// Get function list by employee's id and project id
        /// </summary>
        /// <param name="empId">Employee's ID</param>
        /// <param name="projectId">Project's ID</param>
        /// <returns>Function list by employee's id and project id</returns>
        /// <response code="200">Function list by employee's id and project id</response>
        /// <response code="401">Employee hasn't logged in yet</response>
        [HttpGet("get-function/{empId}")]
        [ProducesResponseType(typeof(ResponseDataDTO<List<ResponseFunction>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFunctionListByProjectId([FromRoute] long empId, [FromQuery] long? projectId)
        {

            List<ResponseFunction> result = await _taskService.GetFunctionListByProjectId(empId, projectId);
            if (result.Count != 0)
            {
                return Ok(new ResponseDataDTO<List<ResponseFunction>>
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

        /// <summary>
        /// Create new task
        /// </summary>
        /// <param name="requestTaskDTO">New Task Body</param>
        /// <returns>A string if added success or failed</returns>
        /// <response code="200">Function list by employee's id and project id</response>
        /// /// <response code="400">Invalid Request Task DTO</response>
        /// <response code="401">Employee hasn't logged in yet</response>
        [HttpPost("")]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateNewTask([FromBody] RequestTaskDTO requestTaskDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDataDTO<string>
                {
                    Data = DevMessageConstants.ADD_OBJECT_FAILED,
                    Code = StatusCodes.Status400BadRequest,
                    Succeed = false,
                    ErrorMessage = ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage).FirstOrDefault()
                });
            }

            try
            {
                await _taskService.CreateNewTask(requestTaskDTO);
                return Ok(new ResponseDataDTO<string>
                {
                    Data = DevMessageConstants.ADD_OBJECT_SUCCESS,
                    Code = StatusCodes.Status200OK,
                    Succeed = true,
                });
            } 
            catch(Exception ex)
            {
                return BadRequest(new ResponseDataDTO<string>
                {
                    Data = DevMessageConstants.ADD_OBJECT_FAILED,
                    Code = StatusCodes.Status400BadRequest,
                    Succeed = false,
                    ErrorMessage = ex.Message
                });
            }
            
        }

        [HttpPut("{taskId}")]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseDataDTO<string>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateTask([FromRoute] long taskId, [FromBody] RequestTaskDTO requestTask)
        {

            // if request is invalid
            if (!ModelState.IsValid)
            {
                var error = ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                }).ToString();

                return BadRequest(new ResponseDataDTO<string>
                {
                    Data = DevMessageConstants.NOTIFICATION_UPDATE_FAILED,
                    Succeed = false,
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = error
                });
            }

            try
            {
                await _taskService.UpdateTask(taskId, requestTask);

                return Ok(new ResponseDataDTO<string>
                {
                    Data = DevMessageConstants.NOTIFICATION_UPDATE_SUCCESS,
                    Succeed = true,
                    Code = StatusCodes.Status200OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDataDTO<string>
                {
                    Succeed = false,
                    Code = StatusCodes.Status400BadRequest,
                    ErrorMessage = ex.Message
                });
            }

        }

    }
}
