using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            var CommandReadDtoList = _mapper.Map<IEnumerable<CommandReadDto>>(commandItems);

            return Ok(CommandReadDtoList);
        }

        // GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItems = _repository.GetCommandById(id);
            if (commandItems != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItems));
            }
            return NotFound();
        }

        // POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            // Mapping Profile must exist first to specify the relationship
            // Source is the commandCreateDto, mapped to the target Command object
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // GetCommandById is referring to the Name in the route 'Name'
            // This line returns a 201 status. 
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        // PUT api/commands{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            // The DbContext knows when the 'commandModelFromRepo' is updated/mapped with new
            // values from the source 'commandUpdateDto', therefore no repo methods are not needed.
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            // However, calling the empty repo method is still 'good practice',
            // even though it doesn't do anything.
            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
    }
}
