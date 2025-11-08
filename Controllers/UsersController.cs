using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using UserManagementAPI.Hubs;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHubContext<UserHub> _hubContext;

        public UsersController(IUserService userService, IHubContext<UserHub> hubContext)
        {
            _userService = userService;
            _hubContext = hubContext;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] string? search)
        {
            IEnumerable<User> users;

            if (!string.IsNullOrWhiteSpace(search))
            {
                // ????? (?????? ???? ??????)
                users = await _userService.GetUsersBySearchAsync(search);
            }
            else
            {
                users = await _userService.GetAllUsersAsync();
            }

            return Ok(users.Take(100)); // ?? ????? ???? ??? 100 ????? ???
        }


        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User niet gevonden" });

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userService.CreateUserAsync(user);
            
            // SignalR notificatie verzenden naar alle clients
            await _hubContext.Clients.All.SendAsync("UserChanged");
            
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, user);
            if (updatedUser == null)
                return NotFound(new { message = "User niet gevonden" });

            // SignalR notificatie verzenden naar alle clients
            await _hubContext.Clients.All.SendAsync("UserChanged");

            return Ok(updatedUser);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
                return NotFound(new { message = "User niet gevonden" });

            // SignalR notificatie verzenden naar alle clients
            await _hubContext.Clients.All.SendAsync("UserChanged");

            return NoContent();
        }
    }
}

