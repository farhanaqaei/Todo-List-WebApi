namespace TodoList.Domain.TodoAggregate.Dtos;

public class CreateTaskDTO
{
    public string Title { get; set; }
    public string Discreption { get; set; }
    public bool IsCompleted { get; set; } = false;
}
