using TodoList.Domain.Common.Dtos;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.Domain.TodoAggregate.Entities;

namespace TodoList.Application.TodoServices.Interfaces;

public interface ITodoService
{
	Task<ResultDTO<Todo>> CreateTask(CreateTaskDTO input, long userId);
	Task<ResultDTO<List<Todo>>> GetAllTasks();
	Task<ResultDTO<Todo>> GetTaskById(long id);
	Task<ResultDTO<List<Todo>>> GetTasksByUserName(string userName);
	Task<ResultDTO<List<Todo>>> GetTasksByUserId(long userId);
	Task<ResultDTO<Todo>> UpdateTask(UpdateTaskDTO input, long userId);
	Task<ResultDTO<Todo>> DeleteTask(long todoId, long userId);
}
