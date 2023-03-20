using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
            
        }

        [HttpGet("server-error")]
        public ActionResult<string> servererror()
        {
             var thing=_context.Users.Find(-1);
             var things=thing.ToString();

             return things;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> Getsecrets()
        {
            return "Secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUsers> GetNotFound()
        {
            var thing=_context.Users.Find(-1);

            if(thing==null) return new NotFoundObjectResult("User not found");

            return thing;

        }
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return new BadRequestObjectResult("this is bad request");
        }
        
    }
}