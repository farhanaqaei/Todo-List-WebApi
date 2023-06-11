using Moq;
using TodoList.Application.TodoServices.Implementations;
using TodoList.Domain.Common.Interfaces;
using TodoList.Domain.TodoAggregate.Dtos;
using TodoList.Domain.TodoAggregate.Entities;
using TodoList.Domain.UserAggregate.Entities;
using Xunit;

namespace TodoList.UnitTests.TodoTests;

public class TodoConstructor
{
    private Todo CreateTodo()
    {
        return new Todo();
    }

    [Fact]
    public void Todo_Should_Set_Default_IsComplete_To_False()
    {
        // Arrange
        var _testTodo = CreateTodo();

        // Assert
        Assert.False(_testTodo.IsCompleted);
    }

    [Fact]
    public void Todo_Should_Set_Properties_Correctly()
    {
        // Arrange
        var userId = 123;
        var title = "Test Todo";
        var description = "This is a test todo";

        // Act
        var todo = new Todo
        {
            UserId = userId,
            Title = title,
            Description = description
        };

        // Assert
        Assert.Equal(userId, todo.UserId);
        Assert.Equal(title, todo.Title);
        Assert.Equal(description, todo.Description);
        Assert.False(todo.IsCompleted);
    }

    [Fact]
    public void Todo_Should_Be_Completed_When_Setting_IsCompleted_To_True()
    {
        // Arrange
        var todo = new Todo();

        // Act
        todo.IsCompleted = true;

        // Assert
        Assert.True(todo.IsCompleted);
    }

    [Fact]
    public void Todo_Should_Not_Be_Equal_To_Another_Todo_With_Different_Id()
    {
        // Arrange
        var todo1 = new Todo { Id = 1 };
        var todo2 = new Todo { Id = 2 };

        // Assert
        Assert.NotEqual(todo1, todo2);
    }

    [Fact]
    public async Task CreateTask_Should_Return_Successful_Result_With_Created_Todo()
    {
        // Arrange
        var todoRepo = new Mock<IGenericRepository<Todo>>();
        var userRepo = new Mock<IGenericRepository<User>>();
        var todoService = new TodoService(todoRepo.Object, userRepo.Object); // Create an instance of your TodoService implementation
        var input = new CreateTaskDTO
        {
            Title = "title",
            Description = "description",
        };
        var userId = 123; // Set a sample user ID

        // Act
        var result = await todoService.CreateTask(input, userId);

        // Assert
        Assert.True(result.Succeeded);
        Assert.NotNull(result.Message);
    }
}
