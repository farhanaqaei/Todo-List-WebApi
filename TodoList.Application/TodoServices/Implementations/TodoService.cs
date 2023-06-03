using TodoList.Application.TodoServices.Interfaces;
using TodoList.Domain.Common.Dtos;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.Domain.TodoAggregate.Entities;

namespace TodoList.Application.TodoServices.Implementations;

public class TodoService : ITodoService
{
	public Task<ResultDTO<Todo>> CreateTask(CreateTaskDTO input, long userId)
	{
		throw new NotImplementedException();
	}

	public Task<ResultDTO<Todo>> GetAllTasks()
	{
		throw new NotImplementedException();
	}

	public Task<ResultDTO<Todo>> GetTasksByUserId(long userId)
	{
		throw new NotImplementedException();
	}

	public Task<ResultDTO<Todo>> GetTasksByUsername(string username)
	{
		throw new NotImplementedException();
	}

	public Task<ResultDTO<Todo>> UpdateTask(UpdateTaskDTO input, long userId)
	{
		throw new NotImplementedException();
	}

	public Task<ResultDTO<Todo>> DeleteTask(long todoId, long userId)
	{
		throw new NotImplementedException();
	}
}
