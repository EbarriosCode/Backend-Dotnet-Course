using AdminProjectsDemo.DTOs.Beneficiaries.Request;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.Beneficiaries;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BeneficiariesController : ControllerBase
    {
        private readonly IBeneficiaryHandler _beneficiaryHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public BeneficiariesController(IBeneficiaryHandler beneficiaryHandler, IConfiguration configuration, IMapper mapper)
        {
            this. _beneficiaryHandler = beneficiaryHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Beneficiario[]>> Get()
        {
            try
            {
                var beneficiaries = await this._beneficiaryHandler.GetAsync(null, string.Empty);

                return Ok(beneficiaries);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{beneficiaryId:int}")]
        public async Task<ActionResult<Beneficiario>> GetById(int beneficiaryId)
        {
            try
            {
                if (beneficiaryId <= 0)
                    throw new ArgumentException("Invalid BeneficiaryId");

                var beneficiary = await this._beneficiaryHandler.GetByIdAsync(beneficiaryId);

                if (beneficiary == null)
                    return NotFound();

                return Ok(beneficiary);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] BeneficiaryCreationRequest beneficiaryCreationRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var beneficiary = this._mapper.Map<Beneficiario>(beneficiaryCreationRequest);
                var rowsAffected = await this._beneficiaryHandler.CreateAsync(beneficiary);

                return rowsAffected > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Beneficiary/{beneficiary.BeneficiarioID}", new { BeneficiarioID = beneficiary.BeneficiarioID }) 
                                              : BadRequest("Error when creating the beneficiary");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{beneficiaryId:int}")]
        public async Task<IActionResult> Update([FromRoute] int beneficiaryId, [FromBody] BeneficiaryUpdateRequest beneficiaryUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (beneficiaryId != beneficiaryUpdateRequest.BeneficiarioID)
                    throw new ArgumentException("beneficiaryId doesn't match with the beneficiaryId of URL");

                var beneficiary = this._mapper.Map<Beneficiario>(beneficiaryUpdateRequest);
                await this._beneficiaryHandler.UpdateAsync(beneficiary);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{beneficiaryId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int beneficiaryId)
        {
            try
            {
                if (beneficiaryId <= 0)
                    throw new ArgumentException("Invalid BeneficiaryId");

                var existBeneficiary = await this._beneficiaryHandler.ExistRecordAsync(x => x.BeneficiarioID == beneficiaryId);

                if (!existBeneficiary)
                    return NotFound();

                await this._beneficiaryHandler.DeleteAsync(beneficiaryId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
