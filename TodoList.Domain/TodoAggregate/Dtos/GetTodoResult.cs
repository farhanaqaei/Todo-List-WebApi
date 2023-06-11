namespace TodoList.Domain.TodoAggregate.Dtos;

public class GetTodoResult
{
	public long Id { get; set; }
	public long UserId { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }
	public bool IsCompleted { get; set; }
}
