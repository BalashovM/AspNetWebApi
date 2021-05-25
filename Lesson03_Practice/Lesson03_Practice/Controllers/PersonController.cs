using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Lesson03_Practice.Domain.Interfaces;
using Lesson03_Practice.Models;
using Lesson03_Practice.Models.Dto;

namespace Lesson03_Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class PersonController: ControllerBase
    {
        private readonly IPersonManager _personManager;
        
        public PersonController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [HttpGet("id/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                Person person = _personManager.GetItem(id);
                PersonDto result = new PersonDto()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Age = person.Age,
                    Company = person.Company
                };
                return Ok(result);
            }
            catch(System.NullReferenceException)
            {
                return NotFound("No Content");
            }
        }
        
        [HttpGet("searchTerm={term}")]
        public IActionResult GetItemByName([FromRoute] string term)
        {
            try
            {
                List<Person> persons = _personManager.GetItemByName(term);
                if (persons == null) return NoContent();
                List<PersonDto> result = new List<PersonDto>();
                foreach (var person in persons)
                {
                    result.Add(new PersonDto()
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Company = person.Company,
                        Age = person.Age,
                        Email = person.Email
                    });
                }
                return Ok(result);
            }
            catch(System.NullReferenceException)
            {
                return NotFound("No Content");
            }
        }
        
        [HttpGet("skip={skip}&take={take}")]
        public IActionResult GetAll([FromRoute] int skip, [FromRoute] int take)
        {
            try
            {
                List<Person> persons = _personManager.GetAll(skip, take);
                List<PersonDto> result = new List<PersonDto>();
                foreach (var person in persons)
                {
                    result.Add(new PersonDto()
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        Company = person.Company,
                        Age = person.Age,
                        Email = person.Email
                    });
                }
                return Ok(result);
            }
            catch(System.NullReferenceException)
            {
                return NotFound("No Content");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PersonDto person)
        {
            var id = _personManager.Create(person);
            return Ok(id);
        }
        
        [HttpPut]
        public IActionResult Update([FromBody] Person person)
        {
            _personManager.Update(person);
            return Ok();
        }
        
        [HttpDelete("id/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _personManager.Delete(id);
            return Ok();
        }
    }
}