using TodoList.Domain.Common.Dtos;
using TodoList.Domain.UserAggregate.Dtos;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.Application.UserServices.Interfaces;

public interface IUserService
{
	Task<ResultDTO<User>> RegisterUser(RegisterUserDTO input);
	Task<bool> IsUserExistsByEmail(string email);
	Task<ResultDTO<User>> GetUserForLogin(LoginUserDTO input);
}
