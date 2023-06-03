using TodoList.Domain.Common.Dtos;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.Application.UserServices.Interfaces;

public interface IUserService
{
	Task<ResultDTO<User>> GetUserByEmail(string email);
}
