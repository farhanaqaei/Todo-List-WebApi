using Microsoft.EntityFrameworkCore;
using TodoList.Application.TodoServices.Interfaces;
using TodoList.Domain.Common.Dtos;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.Domain.TodoAggregate.Entities;
using TodoList.Domain.UserAggregate.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoList.Application.TodoServices.Implementations;

public class TodoService : ITodoService
{
	private readonly IGenericRepository<Todo> _todoRepository;
	private readonly IGenericRepository<User> _userRepository;

	public TodoService(IGenericRepository<Todo> todoRepository, IGenericRepository<User> userRepository)
	{
		_todoRepository = todoRepository;
		_userRepository = userRepository;
	}

	public async Task<ResultDTO<Todo>> CreateTask(CreateTaskDTO input, long userId)
	{
		var newTask = new Todo
		{
			Title = input.Title,
			Description = input.Descreption,
			UserId = userId
		};

		await _todoRepository.AddEntity(newTask);
		await _todoRepository.SaveChanges();

		return new ResultDTO<Todo> { Data = newTask, Message = "task added successfully", Succeeded = true };
	}

	public async Task<ResultDTO<List<Todo>>> GetAllTasks() => new ResultDTO<List<Todo>>
	{
		Data = await _todoRepository.GetQuery().AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync()
	};

	public async Task<ResultDTO<List<Todo>>> GetTasksByUserId(long userId) => new ResultDTO<List<Todo>>
	{
		Data = await _todoRepository.GetQuery().Where(x => x.UserId == userId && x.IsDeleted == false).ToListAsync()
	};

	public async Task<ResultDTO<List<Todo>>> GetTasksByUserName(string userName) => new ResultDTO<List<Todo>>
	{
		Data = await _todoRepository.GetQuery().AsNoTracking().Where(x => x.IsDeleted == false && EF.Functions.Like(x.User.FullName, $"%{userName}%")).ToListAsync()
	};

	public async Task<ResultDTO<Todo>> GetTaskById(long id) => new ResultDTO<Todo> { Data = await _todoRepository.GetEntityById(id) };

	public async Task<ResultDTO<Todo>> UpdateTask(UpdateTaskDTO input, long userId)
	{
		var taskResult = await GetTaskById(input.Id);
		var task = taskResult.Data;
		if (task?.UserId != userId) return new ResultDTO<Todo> { Succeeded = false, Data = task, Message = "forbidden action" };
		
		task.IsDeleted = input.IsDeleted;
		task.IsComplete = input.IsCompleted;
		task.Description = input.Descreption;
		task.Title = input.Title;

		_todoRepository.EditEntity(task);
		await _todoRepository.SaveChanges();

		return new ResultDTO<Todo> { Succeeded = true, Data = task, Message = "task updated successfully" };
	}

	public async Task<ResultDTO<Todo>> DeleteTask(long todoId, long userId)
	{
		var taskResult = await GetTaskById(todoId);
		var task = taskResult.Data;
		if (task?.UserId != userId) return new ResultDTO<Todo> { Succeeded = false, Data = task, Message = "forbidden action" };
		await _todoRepository.DeleteEntity(task.Id);
		await _todoRepository.SaveChanges();
		return new ResultDTO<Todo> { Succeeded = true, Data = task, Message = "task deleted successfully" };
	}
}
