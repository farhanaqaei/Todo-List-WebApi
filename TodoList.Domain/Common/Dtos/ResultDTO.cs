namespace TodoList.Domain.Common.Dtos;

public class ResultDTO<T> where T : class
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}
