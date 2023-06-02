using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.Common.Dtos;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.UserAggregate.Dtos;
using TodoList.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Application.UserServices.Implementations;

public class UserService : IUserService
{
	private readonly IGenericRepository<User> _userRepository;
	private readonly IPasswordHelper _passwordHelper;

	public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
	{
		_userRepository = userRepository;
		_passwordHelper = passwordHelper;
	}

	public async Task<bool> IsUserExistsByEmail(string email)
	{
		return await _userRepository.GetQuery().AsQueryable().AnyAsync(x => x.Email == email);
	}

	public async Task<ResultDTO<User>> RegisterUser(RegisterUserDTO input)
	{
		if (!await IsUserExistsByEmail(input.Email))
		{
			var user = new User
			{
				FullName = input.FullName,
				Email = input.Email,
				Password = _passwordHelper.EncodePasswordMd5(input.Password)
			};

			await _userRepository.AddEntity(user);
			await _userRepository.SaveChanges();

			return new ResultDTO<User> { IsSucceeded = true };
		}

		return new ResultDTO<User> { IsSucceeded = false, Message = "email already exists." };
	}

	public async Task<ResultDTO<User>> GetUserForLogin(LoginUserDTO input)
	{
		var user = await _userRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(x => x.Email == input.Email);
		if (user == null) return new ResultDTO<User> { IsSucceeded = false, Message = "user not found" };
		if (user.Password != _passwordHelper.EncodePasswordMd5(input.Password)) return new ResultDTO<User> { IsSucceeded = false, Message = "email or password is invalid" };
		return new ResultDTO<User> { IsSucceeded = true, Data = user };
	}
}
