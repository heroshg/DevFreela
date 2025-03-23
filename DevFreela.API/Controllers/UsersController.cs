using DevFreela.Application.Commands.ChangePassword;
using DevFreela.Application.Commands.InsertUser;
using DevFreela.Application.Commands.InsertUserSkills;
using DevFreela.Application.Commands.NewLogin;
using DevFreela.Application.Commands.RequestPasswordRecovery;
using DevFreela.Application.Commands.ValidateRecoveryCode;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Post(InsertUserCommand model)
        {
            var result = await _mediator.Send(model);

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(NewLoginCommand model)
        {
            var result = await _mediator.Send(model);
            if(!result.IsSuccess)
            {
                BadRequest(result.Message);
            }

            return Ok(result);
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

        [HttpPost("password-recovery/request")]
        [AllowAnonymous]
        public  async Task<IActionResult> RequestPasswordRecovery(RequestPasswordRecoveryCommand model)
        {

            var result = await _mediator.Send(model);
            if(!result.IsSuccess)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost("password-recovery/validate")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateRecoveryCode(ValidateRecoveryCodeCommand model)
        {
            var result = await _mediator.Send(model);
            if(!result.IsSuccess)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost("password-recovery/change")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand model)
        {

            var result = await _mediator.Send(model);

            if(!result.IsSuccess)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
