using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkills;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _mediator.Send(new GetUserByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post(InsertUserCommand model)
        {
            var result = await _mediator.Send(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, model);
        }

        [HttpPost("{id}/skills")]
        public async Task<IActionResult> PostSkills(int id, InsertUserSkillCommand model)
        {
            var result = await _mediator.Send(model);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = id }, model);
        }

        [HttpPost("{id}/profile-picture")]
        public IActionResult PostProfilePicture(IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            // Processar a imagem

            return Ok(description);
        }
    }
}
