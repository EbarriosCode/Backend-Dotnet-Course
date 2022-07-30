﻿using AdminProjectsDemo.DTOs.Request;
using AdminProjectsDemo.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminProjectsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;        
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager,
                                  RoleManager<IdentityRole> roleManager,
                                  IConfiguration configuration)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IdentityUser[]>> Get()
        {
            try
            {
                var allUsersIdentity = await this._userManager.Users.ToArrayAsync();

                return Ok(allUsersIdentity);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IdentityUser>> Get([FromRoute] string userId)
        {
            try
            {
                var userIdentityById = await this._userManager.FindByIdAsync(userId);

                if (userIdentityById == null)
                    return NotFound("User not found");

                return Ok(userIdentityById);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Create([FromBody] UserCredentials userCredentials)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new IdentityUser()
                {
                    UserName = userCredentials.Email,
                    Email = userCredentials.Email
                };

                var creationUserResult = await this._userManager.CreateAsync(user, userCredentials.Password);

                if (creationUserResult.Succeeded)
                {
                    await this._userManager.AddToRoleAsync(user, "Invitado");

                    var tokenResponse = this.BuildToken(userCredentials);

                    return Created($"https://localhost:7209/api/Accounts/{user.Id}", tokenResponse);
                }               

                return BadRequest(creationUserResult.Errors);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] AccountUpdateRequest accountUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var findUserById = await this._userManager.FindByIdAsync(accountUpdateRequest.Id);

                if (findUserById == null)
                    return NotFound("User not found");

                findUserById.UserName = accountUpdateRequest.UserName;
                findUserById.Email = accountUpdateRequest.Email;

                var userUpdated = await this._userManager.UpdateAsync(findUserById);

                return NoContent();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete([FromRoute] string userId)
        {
            try
            {
                var findUserById = await this._userManager.FindByIdAsync(userId);

                if (findUserById == null)
                    return NotFound("User not found");

                var userDeleted = await this._userManager.DeleteAsync(findUserById);
                
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] UserCredentials userCredentials)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var loginResult = await this._signInManager.PasswordSignInAsync(userCredentials.Email,
                                                                                userCredentials.Password,
                                                                                isPersistent: false,
                                                                                lockoutOnFailure: false);

                if (loginResult.Succeeded)
                    return BuildToken(userCredentials);

                return BadRequest("Invalid Login");
            }
            catch (Exception)
            {
                return BadRequest("Invalid Login");
            }
        }        

        private AuthenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("Email", userCredentials.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpirationDate = expiration
            };
        }
    }
}
