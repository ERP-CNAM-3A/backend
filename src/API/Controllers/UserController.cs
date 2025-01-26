using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Users.Commands;
using Domain.Entities.Users;
using API.DTO.UserDTOs;
using Application.UseCases.Users.Queries;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region GET

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var query = new GetAllUsers_Query();
                List<User> users = await _mediator.Send(query);
                return Ok(users.Select(user => new UserDTO(user)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retrieves a user by their unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The details of the user with the specified ID.</returns>
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserById_Query(id);
            try
            {
                User user = await _mediator.Send(query);
                return Ok(new UserDTO(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region POST

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="input">The user data transfer object containing information for creating the user.</param>
        /// <returns>The details of the newly created user.</returns>
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateExternalCard(UserDTO input)
        {
            var command = new CreateUser_Command(
                input.FirstName, input.LastName, input.Email, input.Phone);
            try
            {
                User createdUser = await _mediator.Send(command);
                return Ok(new UserDTO(createdUser));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region PUT

        /// <summary>
        /// Updates the details of an existing user by their unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <param name="input">The user data transfer object containing the updated details of the user.</param>
        /// <returns>The details of the updated user.</returns>
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDTO input)
        {
            var command = new UpdateUser_Command(id, input.FirstName, input.LastName, input.Email, input.Phone);
            try
            {
                User userUpdated = await _mediator.Send(command);
                return Ok(new UserDTO(userUpdated));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion

        #region DELETE

        /// <summary>
        /// Deletes a user by their unique identifier (ID).
        /// </summary>
        /// <param name="id">The unique identifier of the user to be deleted.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUser_Command(id);
            try
            {
                await _mediator.Send(command);
                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion
    }
}
