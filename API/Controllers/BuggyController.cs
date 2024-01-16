using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context=context;
        }

       [HttpGet("notfound")]
       public ActionResult GetNotFoundRequest() 
       {
        var thing=_context.Products.Find(42);
        if(thing==null)
        return NotFound(new ApiResponse(404));
        
        return Ok();

       }
       
       [HttpGet("servererror")]
       public ActionResult GetServeerError() 
       {
          var thing=_context.Products.Find(42);
          var thinhtoReturn=thing.ToString();
        return Ok();
       }

      [HttpGet("badrequest")]
       public ActionResult GetBadRequest() 
       {
        
        return BadRequest(new ApiResponse(400));
       }

      [HttpGet("badrequest/{id}")]
       public ActionResult GetNotFoundRequest(int id) 
       {
        
        return BadRequest();
       }
       
    }
}