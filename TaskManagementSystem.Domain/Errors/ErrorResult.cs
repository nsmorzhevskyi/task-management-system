namespace TaskManagementSystem.Domain.Errors;

public class ErrorResult
{
    public string? Title { get; set; }
    public int StatusCode { get; set; }
    public string Path { get; set; }
    public string TraceId { get; set; }
}