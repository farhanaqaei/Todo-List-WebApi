using TodoList.Domain.Common.Enums;

namespace TodoList.Domain.Common.Dtos;

public class ResultDTO<T> where T : class
{
    public ResultStatus resultStatus { get; set; } = ResultStatus.Succeded;
    public string? Message { get; set; }
    public T? Data { get; set; }
}
