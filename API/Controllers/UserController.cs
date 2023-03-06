
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class UserController : BaseApiController
    {
       
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUsers>>> GetUsers()
        {
            return  await _dataContext.Users.ToListAsync();

            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUsers>> GetUsers(int Id)
        {
            return await _dataContext.Users.FindAsync(Id);

        }

        

        
    }
}