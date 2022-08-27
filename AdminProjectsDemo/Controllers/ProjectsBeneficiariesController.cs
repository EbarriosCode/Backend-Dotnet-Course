using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.ProjectBeneficiary;
using AdminProjectsDemo.Services.ProjectExecutor;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectsBeneficiariesController : ControllerBase
    {
        private readonly IProjectBeneficiaryHandler _projectBeneficiaryHandler;
        private readonly IConfiguration _configuration;

        public ProjectsBeneficiariesController(IProjectBeneficiaryHandler projectBeneficiaryHandler, IConfiguration configuration)
        {
           this. _projectBeneficiaryHandler = projectBeneficiaryHandler;
           this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<ProyectoBeneficiario[]>> Get()
        {
            try
            {
                var projectBeneficiaries = await this._projectBeneficiaryHandler.GetAsync();

                return Ok(projectBeneficiaries);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{projectId:int}/{beneficiaryId:int}")]
        public async Task<ActionResult<ProyectoBeneficiario>> GetByProjectIdAndBeneficiaryId([FromRoute] int projectId, [FromRoute] int beneficiaryId)
        {
            try
            {
                if (projectId <= 0 || beneficiaryId <= 0)
                    throw new ArgumentException("ProjectId or BeneficiaryId is Invalid");

                var projectBeneficiary = await this._projectBeneficiaryHandler.GetByProjectIdAndBeneficiaryIdAsync(projectId, beneficiaryId);

                if (projectBeneficiary == null)
                    return NotFound();

                return Ok(projectBeneficiary);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }       

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ProyectoBeneficiario projectBeneficiary)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rowsAffected = await this._projectBeneficiaryHandler.CreateAsync(projectBeneficiary);

                return rowsAffected > 0 
                                        ? Created($"{this._configuration["HostURL"]}/ProjectsBeneficiaries/{projectBeneficiary.ProyectoID}/{projectBeneficiary.BeneficiarioID}", 
                                                  new { ProjectId = projectBeneficiary.ProyectoID, BeneficiaryId = projectBeneficiary.BeneficiarioID }) 
                                        : BadRequest("Error when creating the projectExecutor relation");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
