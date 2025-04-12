using DemoMapster.DTOs;
using DemoMapster.Model;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoMapster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMapper _mapper;

        public PersonController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(CreatePerson createPerson)
        {
            var personSaved = createPerson.Adapt<Person>();

            var person = new Person();

            createPerson.Adapt(person);

            PersonRepository.SavePerson(personSaved);

            return Ok(PersonRepository.SavePerson(person));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var personsViewModel = PersonRepository.GetPerson().ProjectToType<PersonViewModel>().ToList();
            return Ok(personsViewModel);    
        }

    }
}
