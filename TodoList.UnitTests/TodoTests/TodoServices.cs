using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using TodoList.Application.TodoServices.Implementations;
using TodoList.Application.TodoServices.Interfaces;
using TodoList.Domain.Common.Dtos;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.Domain.TodoAggregate.Entities;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.UnitTests.TodoTests;

public class TodoServices
{

	[Fact]
	public async Task CreateTask_Should_Return_Successful_Result_With_Created_Todo()
	{
		// Arrange
		var todoRepo = new Mock<IGenericRepository<Todo>>();
		var userRepo = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepo.Object, userRepo.Object);
		var input = new CreateTaskDTO
		{
			Title = "title",
			Description = "description",
		};
		var userId = 123; 

		// Act
		var result = await todoService.CreateTask(input, userId);

		// Assert
		Assert.True(result.Succeeded);
		Assert.NotNull(result.Message);
	}

	[Fact]
	public async Task GetTaskById_Should_Return_Null_For_Nonexistent_Id()
	{
		// Arrange
		var todoRepositoryMock = new Mock<IGenericRepository<Todo>>();
		var userRepositoryMock = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepositoryMock.Object, userRepositoryMock.Object);

		var taskId = 123;
		todoRepositoryMock.Setup(repo => repo.GetEntityById(taskId))
			.ReturnsAsync((Todo)null);

		// Act
		var result = await todoService.GetTaskById(taskId);

		// Assert
		Assert.Null(result.Data);
	}

	[Fact]
	public async Task GetTaskById_Should_Return_Task_By_Id()
	{
		// Arrange
		var todoRepositoryMock = new Mock<IGenericRepository<Todo>>();
		var userRepositoryMock = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepositoryMock.Object, userRepositoryMock.Object);

		var taskId = 123;
		var todo = new Todo { Id = taskId, UserId = 1, Title = "Task 1", Description = "Description 1", IsComplete = false };
		todoRepositoryMock.Setup(repo => repo.GetEntityById(taskId))
			.ReturnsAsync(todo);

		// Act
		var result = await todoService.GetTaskById(taskId);

		// Assert
		Assert.NotNull(result.Data);
		Assert.Equal(taskId, result.Data.Id);
	}

	[Fact]
	public async Task CreateTask_Should_Return_Successful_Result_With_Valid_Input()
	{
		// Arrange
		var todoRepositoryMock = new Mock<IGenericRepository<Todo>>();
		var userRepositoryMock = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepositoryMock.Object, userRepositoryMock.Object);

		var input = new CreateTaskDTO
		{
			Title = "New Task",
			Description = "Task description",
		};
		var userId = 1;

		todoRepositoryMock.Setup(repo => repo.AddEntity(It.IsAny<Todo>()))
			.Callback<Todo>(todo =>
			{
				todo.UserId = userId;
			})
			.Returns(Task.CompletedTask);
		todoRepositoryMock.Setup(repo => repo.SaveChanges())
			.Returns(Task.CompletedTask);

		// Act
		var result = await todoService.CreateTask(input, userId);

		// Assert
		Assert.True(result.Succeeded);
		Assert.Equal("task added successfully", result.Message);
	}

	[Fact]
	public async Task DeleteTask_Should_Return_Successful_Result_With_Valid_Input()
	{
		// Arrange
		var todoRepositoryMock = new Mock<IGenericRepository<Todo>>();
		var userRepositoryMock = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepositoryMock.Object, userRepositoryMock.Object);

		var todoId = 1;
		var userId = 1;

		var sampleTask = new Todo
		{
			Id = todoId,
			UserId = userId,
			Title = "Sample Task",
			Description = "Sample description",
			IsComplete = false
		};
		todoRepositoryMock.Setup(repo => repo.GetEntityById(todoId))
			.ReturnsAsync(sampleTask);

		todoRepositoryMock.Setup(repo => repo.DeleteEntity(todoId))
			.Returns(Task.CompletedTask);
		todoRepositoryMock.Setup(repo => repo.SaveChanges())
			.Returns(Task.CompletedTask);

		// Act
		var result = await todoService.DeleteTask(todoId, userId);

		// Assert
		Assert.True(result.Succeeded);
		Assert.Equal("task deleted successfully", result.Message);
	}

	[Fact]
	public async Task UpdateTask_Should_Return_Successful_Result_With_Valid_Input()
	{
		// Arrange
		var todoRepositoryMock = new Mock<IGenericRepository<Todo>>();
		var userRepositoryMock = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepositoryMock.Object, userRepositoryMock.Object);

		var todoId = 1;
		var userId = 1;

		var updateTaskDto = new UpdateTaskDTO
		{
			Id = todoId,
			Title = "Updated Task",
			Description = "Updated description",
			IsCompleted = true,
			IsDeleted = false
		};

		var sampleTask = new Todo
		{
			Id = todoId,
			UserId = userId,
			Title = "Sample Task",
			Description = "Sample description",
			IsComplete = false
		};
		todoRepositoryMock.Setup(repo => repo.GetEntityById(todoId))
			.ReturnsAsync(sampleTask);

		todoRepositoryMock.Setup(repo => repo.EditEntity(It.IsAny<Todo>()));
		todoRepositoryMock.Setup(repo => repo.SaveChanges())
			.Returns(Task.CompletedTask);

		// Act
		var result = await todoService.UpdateTask(updateTaskDto, userId);

		// Assert
		Assert.True(result.Succeeded);
		Assert.Equal("task updated successfully", result.Message);
	}

	[Fact]
	public async Task GetTaskById_Should_Return_Todo_With_Valid_Id()
	{
		// Arrange
		var todoRepositoryMock = new Mock<IGenericRepository<Todo>>();
		var userRepositoryMock = new Mock<IGenericRepository<User>>();
		var todoService = new TodoService(todoRepositoryMock.Object, userRepositoryMock.Object);

		var todoId = 1;

		// Mock the GetEntityById method to return a sample task
		var sampleTask = new Todo
		{
			Id = todoId,
			UserId = 1,
			Title = "Sample Task",
			Description = "Sample description",
			IsComplete = false
		};
		todoRepositoryMock.Setup(repo => repo.GetEntityById(todoId))
			.ReturnsAsync(sampleTask);

		// Act
		var result = await todoService.GetTaskById(todoId);

		// Assert
		Assert.NotNull(result.Data);
		Assert.Equal(todoId, result.Data.Id);
	}
}
