using AdminProjectsDemo.DTOs.ProjectsBeneficiaries.Request;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.ProjectBeneficiary;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProjectsBeneficiariesController(IProjectBeneficiaryHandler projectBeneficiaryHandler, IConfiguration configuration, IMapper mapper)
        {
            this._projectBeneficiaryHandler = projectBeneficiaryHandler;
            this._configuration = configuration;
            this._mapper = mapper;
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
        public async Task<ActionResult<int>> Create([FromBody] ProjectBeneficiaryCreationRequest projectBeneficiaryCreationRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var projectBeneficiary = this._mapper.Map<ProyectoBeneficiario>(projectBeneficiaryCreationRequest);
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
