using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.Activities;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityHandler _activityHandler;
        private readonly IConfiguration _configuration;

        public ActivitiesController(IActivityHandler activityHandler, IConfiguration configuration)
        {
           this. _activityHandler = activityHandler;
           this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<Actividad[]>> Get()
        {
            try
            {
                var activities = await this._activityHandler.GetAsync();

                return Ok(activities);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{activityId:int}")]
        public async Task< ActionResult<Actividad>> GetById(int activityId)
        {
            try
            {
                if (activityId <= 0)
                    return BadRequest("Invalid ActivityId");

                var activity = await this._activityHandler.GetByIdAsync(activityId);

                if (activity == null)
                    return NotFound();

                return Ok(activity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Actividad activity)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var activityCreated = await this._activityHandler.CreateAsync(activity);

                return activityCreated > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Activities/{activity.ActividadID}", new { ActivityId = activity.ActividadID }) 
                                              : BadRequest("Error when creating the activity");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{activityId:int}")]
        public async Task<IActionResult> Update([FromRoute] int activityId, [FromBody] Actividad activity)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (activityId != activity.ActividadID)
                    return BadRequest("activityId doesn't match with the activityId of URL");

                await this._activityHandler.UpdateAsync(activity);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{activityId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int activityId)
        {
            try
            {
                var existActivity = await this._activityHandler.ExistRecordAsync(activityId);

                if (!existActivity)
                    return NotFound();

                await this._activityHandler.DeleteAsync(activityId);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }
    }
}
