using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.Executors;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExecutorsController : ControllerBase
    {
        private readonly IExecutorHandler _executorHandler;
        private readonly IConfiguration _configuration;

        public ExecutorsController(IExecutorHandler executorHandler, IConfiguration configuration)
        {
           this. _executorHandler = executorHandler;
           this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<Executor[]>> Get()
        {
            try
            {
                var executors = await this._executorHandler.GetAsync();

                return Ok(executors);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{executorId:int}")]
        public async Task<ActionResult<Executor>> GetById(int executorId)
        {
            try
            {
                if (executorId <= 0)
                    return BadRequest("Invalid ExecutorId");

                var executor = await this._executorHandler.GetByIdAsync(executorId);

                if (executor == null)
                    return NotFound();

                return Ok(executor);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Executor executor)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rowsAffected = await this._executorHandler.CreateAsync(executor);

                return rowsAffected > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Executor/{executor.ExecutorId}", new { ExecutorId = executor.ExecutorId }) 
                                              : BadRequest("Error when creating the executor");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{executorId:int}")]
        public async Task<IActionResult> Update([FromRoute] int executorId, [FromBody] Executor executor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (executorId != executor.ExecutorId)
                    return BadRequest("executorId doesn't match with the executorId of URL");

                await this._executorHandler.UpdateAsync(executor);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{executorId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int executorId)
        {
            try
            {
                var existExecutor = await this._executorHandler.ExistRecordAsync(executorId);

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
