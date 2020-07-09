using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TpjProject.API.Data;
using Microsoft.EntityFrameworkCore;
using TpjProject.API.DTOs;
using TpjProject.API.Models;
using System.Security.Claims ; 
using Microsoft.IdentityModel.Tokens; 
using System.Text; 
using Microsoft.Extensions.Configuration; 
using System.IdentityModel.Tokens.Jwt; 
namespace TpjProject.API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthRepo _repo;
         public readonly IConfiguration _config;
        public AuthController(IAuthRepo repo, IConfiguration config)
        {
            _repo = repo;
            _config = config ; 
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userreg)
        {
            userreg.UserName = userreg.UserName.ToLower();
            if (await _repo.UserExist(userreg.UserName))
                return BadRequest("User already exists");
            var userToCreate = new User
            {
                UserName = userreg.UserName
            };
            var CreatedUser = await _repo.Register(userToCreate, userreg.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userlogin)
        {

            var userForLogin =await  _repo.Login(userlogin.UserName, userlogin.Password);
            if (userForLogin == null)
                return Unauthorized();
            var claims = new [] {

             new Claim (ClaimTypes.NameIdentifier, userForLogin.Id.ToString()),
             new Claim (ClaimTypes.Name, userForLogin.UserName)
         }; 
           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
           var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
           var tokenDescriptor = new SecurityTokenDescriptor(){
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                    
           };
           var tokenHandler = new JwtSecurityTokenHandler(); 
            var token = tokenHandler.CreateToken(tokenDescriptor );
            return Ok(new {
                Token = tokenHandler.WriteToken(token)
            });
        }

    

    }

}