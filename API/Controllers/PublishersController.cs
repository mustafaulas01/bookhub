using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Intefaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PublishersController:BaseApiController
    {
        IPublisherRepository _publisherRepository;
        public PublishersController(IPublisherRepository publisherRepository)
        {

            _publisherRepository=publisherRepository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Publisher>>> GetPublishers()
        {  
            var publishers= await _publisherRepository.GetAllPublishersSync();

            if(publishers.Any())
            return Ok(publishers);
            else
            return NotFound(404);

        }
        
    }
}