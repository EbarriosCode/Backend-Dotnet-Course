using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Services.Projects;
using Microsoft.AspNetCore.Mvc;
using AdminProjectsDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper;
using AdminProjectsDemo.DTOs.Projects.Request;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectHandler _projectHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectHandler projectHandler, IConfiguration configuration, IMapper mapper)
        {
            this. _projectHandler = projectHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Proyecto[]>> Get()
        {
            try
            {
                var projects = await this._projectHandler.GetAsync(null, "Actividades");

                return Ok(projects);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{projectId:int}")]
        public async Task<ActionResult<Proyecto>> GetById(int projectId)
        {
            try
            {
                if (projectId <= 0)
                    throw new ArgumentException("Invalid ProjectId");

                var project = await this._projectHandler.GetByIdAsync(projectId);

                if (project == null)
                    return NotFound();

                return Ok(project);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProjectCreationRequest projectCreateRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var project = this._mapper.Map<Proyecto>(projectCreateRequest);

                var projectCreated = await this._projectHandler.CreateAsync(project);

                return projectCreated > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Projects/{project.ProyectoID}", new { ProjectId = project.ProyectoID }) 
                                              : BadRequest("Error when creating the project");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{projectId:int}")]
        public async Task<IActionResult> Update([FromRoute] int projectId, [FromBody] ProjectUpdateRequest projectUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                

                if (projectId != projectUpdateRequest.ProyectoID)
                    throw new ArgumentException("projectId doesn't match with the projectId of URL");

                var project = this._mapper.Map<Proyecto>(projectUpdateRequest);
                await this._projectHandler.UpdateAsync(project);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int projectId)
        {
            try
            {
                if (projectId <= 0)
                    throw new ArgumentException("Invalid ProjectId");

                var existProject = await this._projectHandler.ExistRecordAsync(x => x.ProyectoID == projectId);

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
