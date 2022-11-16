using CleanArchMvc.API.DTOs;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authentication;

        public TokenController(IAuthenticate authentication)
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _authentication.AuthenticateAsync(loginDTO.Email, loginDTO.Password);

            if (result)
                //return GenerateToken(loginDTO);
                return Ok($"User {loginDTO.Email} login successfully");

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return BadRequest(ModelState);
        }
    }
}
