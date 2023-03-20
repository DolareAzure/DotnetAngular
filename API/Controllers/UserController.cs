
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
       
        
        public IuserRepository _UserRepo { get; set; }
        private readonly IMapper _mapper;

        public UsersController(IuserRepository userRepo,IMapper mapper)
        {
            _mapper = mapper;
            _UserRepo = userRepo;
            
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<MemberDto>> GetUsers()
        {
            var user= await _UserRepo.GetUsersAsync();
            var usersToreturn=_mapper.Map<IEnumerable<MemberDto>>(user);

            return usersToreturn;

            
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUsers>> GetUsers(int Id)
        // {
        //     return await _UserRepo.GetUserByIdAsync(Id);

        // }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUsers(string username)
        {
            return await _UserRepo.GetMemberByUsernameAsync(username);
        }

        

        
    }
}