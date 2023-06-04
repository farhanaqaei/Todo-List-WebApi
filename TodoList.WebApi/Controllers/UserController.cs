using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.UserAggregate.Dtos;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.WebApi.Controllers;

[Route("user")]
public class UserController : Controller
{
	private readonly UserManager<User> _userManager;
	private readonly IUserService _userService;
	private readonly ITokenService _tokenService;

	public UserController
		(
		IUserService userService,
		IConfiguration config,
		UserManager<User> userManager,
		ITokenService tokenService
		)
	{
		_userService = userService;
		_userManager = userManager;
		_tokenService = tokenService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterByIdentity([FromBody] RegisterUserDTO input)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var result = await _userManager.CreateAsync(new User
		{
			UserName = input.Email,
			FullName = input.FullName,
			Email = input.Email
		}, input.Password);

		if (result.Succeeded) return Ok(result);

		return BadRequest(ModelState);
	}

	[HttpPost("login")]
	public async Task<IActionResult> LoginByIdentity([FromBody] LoginUserDTO input)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var managedUser = await _userManager.FindByEmailAsync(input.Email);

		if (managedUser == null)
		{
			return BadRequest("Bad credentials");
		}

		var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, input.Password);

		if (!isPasswordValid)
		{
			return BadRequest("Bad credentials");
		}

		var user = await _userService.GetUserByEmail(input.Email);

		if (user.Succeeded == false)
			return Unauthorized();

		var accessToken = _tokenService.CreateToken(user.Data);

		return Ok(new LoginResultDTO { Token = accessToken });
	}
}
