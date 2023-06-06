using TodoList.Application.UserServices.Interfaces;
using TodoList.Domain.Common.Dtos;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Application.UserServices.Implementations;

public class UserService : IUserService
{
	private readonly IGenericRepository<User> _userRepository;

	public UserService(IGenericRepository<User> userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<ResultDTO<User>> GetUserByEmail(string email)
	{
		var user = await _userRepository.GetQuery().FirstOrDefaultAsync(x => x.Email == email);
		return new ResultDTO<User> { Data = user, Succeeded = true };
	}

	public async ValueTask DisposeAsync()
	{
		await _userRepository.DisposeAsync();
	}
}
