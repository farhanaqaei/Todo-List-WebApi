namespace TodoList.Domain.TodoAggregate.Dtos;

public class UpdateTaskDTO : CreateTaskDTO
{
    public long Id { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsDeleted { get; set; }
}
