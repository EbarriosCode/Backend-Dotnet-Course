using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Services.Projects;
using Microsoft.AspNetCore.Mvc;
using AdminProjectsDemo.Extensions;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectHandler _projectHandler;
        private readonly IConfiguration _configuration;

        public ProjectsController(IProjectHandler projectHandler, IConfiguration configuration)
        {
           this. _projectHandler = projectHandler;
           this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<Project[]>> Get()
        {
            try
            {
                var projects = await this._projectHandler.GetAsync();

                return Ok(projects);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{projectId:int}")]
        public async Task<ActionResult<Project>> GetById(int projectId)
        {
            try
            {
                if (projectId <= 0)
                    return BadRequest("Invalid ProjectId");

                var project = await this._projectHandler.GetByIdAsync(projectId);

                if (project == null)
                    return NotFound();

                return Ok(project);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Project project)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var projectCreated = await this._projectHandler.CreateAsync(project);

                return projectCreated > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Projects/{project.ProjectId}", new { ProjectId = project.ProjectId }) 
                                              : BadRequest("Error when creating the project");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{projectId:int}")]
        public async Task<IActionResult> Update([FromRoute] int projectId, [FromBody] Project project)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (projectId != project.ProjectId)
                    return BadRequest("projectId doesn't match with the projectId of URL");

                await this._projectHandler.UpdateAsync(project);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int projectId)
        {
            try
            {
                var existProject = await this._projectHandler.ExistRecordAsync(projectId);

                if (!existProject)
                    return NotFound();

                await this._projectHandler.DeleteAsync(projectId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
