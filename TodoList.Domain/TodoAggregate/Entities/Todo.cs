using TodoList.Domain.Common.Entities;
using TodoList.Domain.UserAggregate.Entities;

namespace TodoList.Domain.TodoAggregate.Entities;

public class Todo : BaseEntity
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsComplete { get; set; } = false;

    public User User { get; set; }
}
