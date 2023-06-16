using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.Common.Enums;
using TodoList.Domain.UserAggregate.Dtos;
using TodoList.Domain.UserAggregate.Entities;
using TodoList.WebApi.Extensions;

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
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.WrongData.GetEnumName());
		}

		try
		{
			var result = await _userManager.CreateAsync(new User
			{
				UserName = input.Email,
				FullName = input.FullName,
				Email = input.Email
			}, input.Password);

			if (result.Succeeded) return Ok(result);

			return Problem(statusCode: StatusCodes.Status400BadRequest, title: result.Errors.FirstOrDefault()?.ToString());
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}

	}

	[HttpPost("login")]
	public async Task<IActionResult> LoginByIdentity([FromBody] LoginUserDTO input)
	{
		if (!ModelState.IsValid)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.WrongData.GetEnumName());
		}

		try
		{
			var user = await _userManager.FindByEmailAsync(input.Email);

			if (user == null)
			{
				return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.WrongCredentials.GetEnumName());
			}

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, input.Password);

			if (!isPasswordValid)
			{
				return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.WrongCredentials.GetEnumName());
			}

			var accessToken = _tokenService.CreateToken(user);

			return Ok(new LoginResultDTO { Token = accessToken });
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}
	}
}
