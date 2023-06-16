using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.TodoServices.Interfaces;
using TodoList.Domain.Common.Enums;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.WebApi.Extensions;

namespace TodoList.WebApi.Controllers;

[Route("todo")]
public class TodoController : Controller
{
	private readonly ITodoService _todoService;

	public TodoController(ITodoService todoService)
	{
		_todoService = todoService;
	}

	[HttpPost("create-task"), Authorize]
	public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO input)
	{
		try
		{
			var task = await _todoService.CreateTask(input, User.GetUserId());
			switch (task.resultStatus)
			{
				case ResultStatus.Succeded:
					return Ok(task.Message);
				case ResultStatus.NotForUser:
					return Problem(statusCode: StatusCodes.Status403Forbidden, title: task.Message);
				default:
					return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.WrongData.GetEnumName());
			}
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}
	}

	[HttpGet("get-tasks")]
	public async Task<IActionResult> GetAllTasks([FromQuery] string? userName)
	{
		try
		{
			if (userName != null) return Ok(await _todoService.GetTasksByUserName(userName));
			return Ok(await _todoService.GetAllTasks());
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}
	}

	[HttpGet("get-tasks-by-userid")]
	public async Task<IActionResult> GetAllTasks([FromQuery] long userId)
	{
		try
		{
			return Ok(await _todoService.GetTasksByUserId(userId));
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}
	}

	[HttpPatch("update-task/{todoId}")]
	[Authorize(Policy = "TaskOwnershipPolicy")]
	public async Task<IActionResult> UpdateTask(long todoId, [FromBody] UpdateTaskDTO input)
	{
		try
		{
			var updatedTask = await _todoService.UpdateTask(input);

			switch (updatedTask.resultStatus)
			{
				case ResultStatus.Succeded:
					return Ok(updatedTask.Message);
				case ResultStatus.NotFound:
					return Problem(statusCode: StatusCodes.Status404NotFound, title: updatedTask.Message);
				default:
					return Problem(statusCode: StatusCodes.Status400BadRequest, title: updatedTask.Message);
			}
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}
	}

	[HttpDelete("delete-task")]
	[Authorize(Policy = "TaskOwnershipPolicy")]
	public async Task<IActionResult> DeleteTask([FromQuery] long todoId)
	{
		try
		{
			var deletedTask = await _todoService.DeleteTask(todoId);
			if (deletedTask.resultStatus == ResultStatus.Succeded) return Ok(deletedTask.Message);
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: deletedTask.Message);
		}
		catch (Exception)
		{
			return Problem(statusCode: StatusCodes.Status400BadRequest, title: ResultStatus.Failed.GetEnumName());
		}
	}
}
