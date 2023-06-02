using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.UserAggregate.Dtos;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.WebApi.Controllers;

public class UserController : Controller
{
	private readonly IUserService _userService;
	private readonly IConfiguration _config;

	public UserController(IUserService userService, IConfiguration config)
	{
		_userService = userService;
		_config = config;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register(RegisterUserDTO input)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		var result = await _userService.RegisterUser(input);
		return Ok(result);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginUserDTO input)
	{
		var user = await _userService.GetUserForLogin(input);
		if (user.IsSucceeded)
		{
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("SecretKey").Value));
			var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Data.FullName),
				new Claim(ClaimTypes.Email, input.Email)
			};

			var tokenOptions = new JwtSecurityToken(
				issuer: _config.GetSection("ValidIssuer").Value,
				audience: _config.GetSection("ValidAudience").Value,
				claims: claims,
				expires: DateTime.Now.AddMinutes(1),
				signingCredentials: signingCredentials
				);
			var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
			return Ok(new LoginResultDTO { Token = tokenString });
		}
		return Unauthorized();
	}
}
