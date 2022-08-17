using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.Executors;
using AdminProjectsDemo.Services.ProjectExecutor;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsExecutorsController : ControllerBase
    {
        private readonly IProjectExecutorHandler _projectExecutorHandler;
        private readonly IConfiguration _configuration;

        public ProjectsExecutorsController(IProjectExecutorHandler projectExecutorHandler, IConfiguration configuration)
        {
           this. _projectExecutorHandler = projectExecutorHandler;
           this._configuration = configuration;
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
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{projectId:int}/{executorId:int}")]
        public async Task<ActionResult<Ejecutor>> GetByProjectId([FromRoute] int projectId, [FromRoute] int executorId)
        {
            try
            {
                if (projectId <= 0)
                    return BadRequest("Invalid ProjectId");

                var projectExecutor = await this._projectExecutorHandler.GetByProjectIdAndExecutorIdAsync(projectId, executorId);

                if (projectExecutor == null)
                    return NotFound();

                return Ok(projectExecutor);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }       

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProyectoEjecutor projectExecutor)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rowsAffected = await this._projectExecutorHandler.CreateAsync(projectExecutor);

                return rowsAffected > 0 
                                              ? Created(string.Empty, new { ProjectId = projectExecutor.ProyectoID, ExecutorId = projectExecutor.EjecutorID }) 
                                              : BadRequest("Error when creating the projectExecutor relation");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
