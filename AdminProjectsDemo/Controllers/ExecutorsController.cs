using AdminProjectsDemo.DTOs.Executors.Request;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.Executors;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExecutorsController : ControllerBase
    {
        private readonly IExecutorHandler _executorHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ExecutorsController(IExecutorHandler executorHandler, IConfiguration configuration, IMapper mapper)
        {
            this._executorHandler = executorHandler;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Ejecutor[]>> Get()
        {
            try
            {
                var executors = await this._executorHandler.GetAsync(null, string.Empty);

                return Ok(executors);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{executorId:int}")]
        public async Task<ActionResult<Ejecutor>> GetById(int executorId)
        {
            try
            {
                if (executorId <= 0)
                    throw new ArgumentException("Invalid ExecutorId");

                var executor = await this._executorHandler.GetByIdAsync(executorId);

                if (executor == null)
                    return NotFound();

                return Ok(executor);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ExecutorCreationRequest executorCreationRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var executor = this._mapper.Map<Ejecutor>(executorCreationRequest);
                var rowsAffected = await this._executorHandler.CreateAsync(executor);

                return rowsAffected > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Executor/{executor.EjecutorID}", new { ExecutorId = executor.EjecutorID }) 
                                              : BadRequest("Error when creating the executor");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{executorId:int}")]
        public async Task<IActionResult> Update([FromRoute] int executorId, [FromBody] ExecutorUpdateRequest executorUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (executorId != executorUpdateRequest.EjecutorID)
                    throw new ArgumentException("executorId doesn't match with the executorId of URL");

                var executor = this._mapper.Map<Ejecutor>(executorUpdateRequest);
                await this._executorHandler.UpdateAsync(executor);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{executorId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int executorId)
        {
            try
            {
                if (executorId <= 0)
                    throw new ArgumentException("Invalid ExecutorId");

                var existExecutor = await this._executorHandler.ExistRecordAsync(x => x.EjecutorID == executorId);

                if (!existExecutor)
                    return NotFound();

                await this._executorHandler.DeleteAsync(executorId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
