using AdminProjectsDemo.DTOs.ProjectsExecutors.Request;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.ProjectExecutor;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectsExecutorsController : ControllerBase
    {
        private readonly IProjectExecutorHandler _projectExecutorHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProjectsExecutorsController(IProjectExecutorHandler projectExecutorHandler, IConfiguration configuration, IMapper mapper)
        {
            this._projectExecutorHandler = projectExecutorHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProyectoEjecutor[]>> Get()
        {
            try
            {
                var projectExecutors = await this._projectExecutorHandler.GetAsync();

                return Ok(projectExecutors);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{projectId:int}/{executorId:int}")]
        public async Task<ActionResult<ProyectoEjecutor>> GetByProjectIdAndExecutorId([FromRoute] int projectId, [FromRoute] int executorId)
        {
            try
            {
                if (projectId <= 0 || executorId <= 0)
                    throw new ArgumentException("ProjectId or ExecutorId is Invalid");

                var projectExecutor = await this._projectExecutorHandler.GetByProjectIdAndExecutorIdAsync(projectId, executorId);

                if (projectExecutor == null)
                    return NotFound();

                return Ok(projectExecutor);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }       

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProjectExecutorCreationRequest projectExecutorCreationRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var projectExecutor = this._mapper.Map<ProyectoEjecutor>(projectExecutorCreationRequest);
                var rowsAffected = await this._projectExecutorHandler.CreateAsync(projectExecutor);

                return rowsAffected > 0 
                                        ? Created($"{this._configuration["HostURL"]}/ProjectsExecutors/{projectExecutor.ProyectoID}/{projectExecutor.EjecutorID}", new { ProjectId = projectExecutor.ProyectoID, ExecutorId = projectExecutor.EjecutorID }) 
                                        : BadRequest("Error when creating the projectExecutor relation");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
