using TodoList.Domain.Common.Dtos;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.Domain.TodoAggregate.Entities;

namespace TodoList.Application.TodoServices.Interfaces;

public interface ITodoService
{
	Task<ResultDTO<GetTodoResult>> CreateTask(CreateTaskDTO input, long userId);
	Task<ResultDTO<List<GetTodoResult>>> GetAllTasks();
	Task<ResultDTO<Todo>> GetTaskById(long id);
	Task<ResultDTO<List<GetTodoResult>>> GetTasksByUserName(string userName);
	Task<ResultDTO<List<GetTodoResult>>> GetTasksByUserId(long userId);
	Task<ResultDTO<Todo>> UpdateTask(UpdateTaskDTO input);
	Task<ResultDTO<Todo>> DeleteTask(long todoId);
}
