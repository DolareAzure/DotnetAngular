using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccountController:BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext dataContext,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _dataContext = dataContext;
            
        }

        [AllowAnonymous]
        [HttpPost("register")] 
        public async Task<ActionResult<UserDto>> registerUser(RegisterDto registerDto)
        {
            if(await IsUserExists(registerDto.UserName)) 
            return new BadRequestObjectResult("Username already exists"); //"Username already exists");

            using var hmac=new HMACSHA512();

            var user=new AppUsers
            {
                UserName=registerDto.UserName.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new UserDto{
                UserName=user.UserName,
                Token=_tokenService.CreateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user=await _dataContext.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.UserName);
            
            if(user==null) return new UnauthorizedObjectResult("Invalid username"); //Unauthorized();
            using var hmac=new HMACSHA512(user.PasswordSalt);
            var computehash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0; i<computehash.Length;i++)
            {
                if(computehash[i]!=user.PasswordHash[i]) return new UnauthorizedObjectResult("Invalid password"); 

            }

            return new UserDto{
                UserName=user.UserName,
                Token=_tokenService.CreateToken(user)  
            };


        }


        public async Task<bool> IsUserExists(string userName)
        {
            return await _dataContext.Users.AnyAsync(x=>x.UserName==userName.ToLower());
        }



    }
}