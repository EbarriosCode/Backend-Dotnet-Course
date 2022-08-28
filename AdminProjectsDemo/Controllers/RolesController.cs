using AdminProjectsDemo.DTOs.Roles.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {        
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IdentityRole[]>> Get()
        {
            try
            {
                var allRolesIdentity = await this._roleManager.Roles.ToArrayAsync();

                return Ok(allRolesIdentity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{rolId}")]
        public async Task<ActionResult<IdentityUser>> Get([FromRoute] string rolId)
        {
            try
            {
                var rolIdentityById = await this._roleManager.FindByIdAsync(rolId);

                if (rolIdentityById == null)
                    return NotFound("Rol not found");

                return Ok(rolIdentityById);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IdentityRole>> Create([FromBody] RolCreationRequest rolRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var rol = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = rolRequest.Name,
                    NormalizedName = rolRequest.Name
                };

                var creationRolResult = await this._roleManager.CreateAsync(rol);

                if (creationRolResult.Succeeded)
                    return Created($"{this._configuration["HostURL"]}Roles/{rol.Id}", rol);

                return BadRequest(creationRolResult.Errors);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{rolId}")]
        public async Task<ActionResult> Delete([FromRoute] string rolId)
        {
            try
            {
                var findRolById = await this._roleManager.FindByIdAsync(rolId);
                
                if (findRolById == null)
                    return NotFound("Role not found");

                var userDeleted = await this._roleManager.DeleteAsync(findRolById);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("AssignRoleToUser")]
        public async Task<ActionResult> AssignRoleToUser([FromBody] AssignRoleToUserRequest assignRoleToUserRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userIdentity = await this._userManager.FindByIdAsync(assignRoleToUserRequest.UserId);
                var addRoleToUser = await this._userManager.AddToRoleAsync(userIdentity, assignRoleToUserRequest.RoleName);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
