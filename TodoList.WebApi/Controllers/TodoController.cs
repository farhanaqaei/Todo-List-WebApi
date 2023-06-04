using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.TodoServices.Interfaces;
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
	public async Task<IActionResult> CreateTask([FromBody]CreateTaskDTO input)
	{
		try
		{
			var task = await _todoService.CreateTask(input, User.GetUserId());
			if (task.Succeeded) return Ok(task.Message);
			return BadRequest(task.Data);
		}
		catch (Exception)
		{
			throw;
		}
	}

	[HttpGet("get-tasks")]
	public async Task<IActionResult> GetAllTasks([FromQuery]string? userName)
	{
		if (userName != null) return Ok(await _todoService.GetTasksByUserName(userName));
		return Ok(await _todoService.GetAllTasks());
	}

	[HttpGet("get-tasks-by-userid")]
	public async Task<IActionResult> GetAllTasks([FromQuery]long userId)
	{
		return Ok(await _todoService.GetTasksByUserId(userId));
	}

	[HttpPost("update-task"), Authorize]
	public async Task<IActionResult> UpdateTask([FromBody]UpdateTaskDTO input)
	{
		try
		{
			var updatedTask = await _todoService.UpdateTask(input, User.GetUserId());
			if (updatedTask.Succeeded) return Ok(updatedTask.Message);
			return BadRequest(updatedTask.Data);
		}
		catch (Exception)
		{
			throw;
		}		
	}

	[HttpPost("delete-task"), Authorize]
	public async Task<IActionResult> DeleteTask([FromQuery]long todoId)
	{
		try
		{
			var deletedTask = await _todoService.DeleteTask(todoId, User.GetUserId());
			if (deletedTask.Succeeded) return Ok(deletedTask.Message);
			return BadRequest(deletedTask.Data);
		}
		catch (Exception)
		{
			throw;
		}
	}
}
