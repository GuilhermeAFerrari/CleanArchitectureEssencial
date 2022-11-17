using CleanArchMvc.API.DTOs;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CleanArchMvc.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authentication;
    private readonly IConfiguration _configuration;
    public TokenController(IAuthenticate authentication, IConfiguration configuration)
    {
        _authentication = authentication ?? throw new ArgumentNullException(nameof(authentication));
        _configuration = configuration;
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody] LoginDTO loginDTO)
    {
        var result = await _authentication.RegisterUserAsync(loginDTO.Email, loginDTO.Password);

        if (result)
            return Ok($"User {loginDTO.Email} was creat successfully");

        ModelState.AddModelError(string.Empty, "An error occurred while creating the user");
        return BadRequest(ModelState);
    }

    [HttpPost("LoginUser")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _authentication.AuthenticateAsync(loginDTO.Email, loginDTO.Password);

        if (result)
            return GenerateToken(loginDTO);

        ModelState.AddModelError(string.Empty, "Invalid login attempt");
        return BadRequest(ModelState);
    }

    private UserToken GenerateToken(LoginDTO loginDTO)
    {
        // user declarations
        var claims = new[]
        {
            new Claim("email", loginDTO.Email),
            //new Claim("some value", "what you want"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Jti representa Id do token
        };

        // generate private key for assign token
        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        // generate digital signature
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        // set expiration time
        var expiration = DateTime.UtcNow.AddMinutes(10);

        // generate token
        JwtSecurityToken token = new (
            // issuer
            issuer: _configuration["Jwt:Issuer"],

            // audience
            audience: _configuration["Jwt:Audience"],

            // claims
            claims: claims,

            // expiration date
            expires: expiration,

            // digital signature
            signingCredentials: credentials
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
