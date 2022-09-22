using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using netcoreapi.Models;
using netcoreapi.Data;
using AutoMapper;
using netcoreapi.Dtos;
using Microsoft.AspNetCore.JsonPatch;

namespace netcoreapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(IRepo repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetAllcommands()
        {
            var result = await _repository.getAll();
            return Ok(this._mapper.Map<IEnumerable<CommandReadDto>>(result));
        }
        //get api/commands/{id}
        [HttpGet("{id:int}", Name = "getCommandById")]
        public async Task<ActionResult<CommandReadDto>> getCommandById(int id)
        {
            var result = await _repository.GetCommandById(id);
            if (result != null)
            {
                var resultDto = this._mapper.Map<CommandReadDto>(result);
                return Ok(resultDto);
            }
            return BadRequest("l'information demandée n'existe pas");
        }
        //api/commands
        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> PostCommand(CommandCreateDto cmdDto)
        {
            if (ModelState.IsValid)
            {
                var cmd = _mapper.Map<Command>(cmdDto);
                await this._repository.CreateCommand(cmd);
                await this._repository.SaveChanges();
                var cmdReadDto = _mapper.Map<CommandReadDto>(cmd);
                return CreatedAtRoute(nameof(getCommandById), new
                {
                    Id = cmdReadDto.Id
                }, cmdReadDto);
            }
            return BadRequest();

        }
        //api/commands/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCommand(int id, CommandUpdateDto cmd)
        {
            var commandExist = await this._repository.GetCommandById(id);
            if (commandExist == null)
            {
                return NotFound();
            }
            //we update the object coming from the database with the one from body 
            _mapper.Map(cmd, commandExist);

            await _repository.UpdateCommand(commandExist);
            var created = await _repository.SaveChanges();
            if (!created) return Problem("une erreur serveure est survenue");
            return NoContent();

        }

        //PATCH api/commands/{id}
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> PartialUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchCmd)
        {
            var commandExist = await this._repository.GetCommandById(id);
            if (commandExist == null)
            {
                return NotFound();
            }
            var cmdToPatch = _mapper.Map<CommandUpdateDto>(commandExist);
            //modelstate to make sure the validations are correct
            patchCmd.ApplyTo(cmdToPatch, ModelState);

            //check if the model is valid and send required message if not
            if (!TryValidateModel(cmdToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(cmdToPatch, commandExist);
            await _repository.UpdateCommand(commandExist);
            await _repository.SaveChanges();
            return NoContent();

        }
        //DELETE api/commmands/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCommand(int id)
        {
            var cmd = await _repository.GetCommandById(id);
            if (cmd == null)
            {
                return NotFound();
            }
            await _repository.DeleteCommand(cmd);
            await _repository.SaveChanges();
            return NoContent();
        }

    }


}