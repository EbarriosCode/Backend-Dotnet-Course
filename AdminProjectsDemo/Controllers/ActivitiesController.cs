using AdminProjectsDemo.DTOs.Activities.Request;
using AdminProjectsDemo.Entitites;
using AdminProjectsDemo.Extensions;
using AdminProjectsDemo.Services.Activities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityHandler _activityHandler;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityHandler activityHandler, IConfiguration configuration, IMapper mapper)
        {
            this. _activityHandler = activityHandler;
            this._configuration = configuration;
            this._mapper = mapper;
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
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpGet("{activityId:int}")]
        public async Task< ActionResult<Actividad>> GetById(int activityId)
        {
            try
            {
                if (activityId <= 0)
                    throw new ArgumentException("Invalid ActivityId");

                var activity = await this._activityHandler.GetByIdAsync(activityId);

                if (activity == null)
                    return NotFound();

                return Ok(activity);
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] ActivityCreationRequest activityCreationRequest)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var activity = this._mapper.Map<Actividad>(activityCreationRequest);

                var activityCreated = await this._activityHandler.CreateAsync(activity);

                return activityCreated > 0 
                                              ? Created($"{this._configuration["HostURL"]}/Activities/{activity.ActividadID}", new { ActivityId = activity.ActividadID }) 
                                              : BadRequest("Error when creating the activity");
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpPut("{activityId:int}")]
        public async Task<IActionResult> Update([FromRoute] int activityId, [FromBody] ActivityUpdateRequest activityUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (activityId != activityUpdateRequest.ActividadID)
                    throw new ArgumentException("activityId doesn't match with the activityId of URL");

                var activity = this._mapper.Map<Actividad>(activityUpdateRequest);
                await this._activityHandler.UpdateAsync(activity);

                return NoContent();
            }
            catch (Exception exception)
            {
                return exception.ConvertToActionResult(HttpContext);
            }
        }

        [HttpDelete("{activityId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int activityId)
        {
            try
            {
                if (activityId <= 0)
                    throw new ArgumentException("Invalid ActivityId");

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
