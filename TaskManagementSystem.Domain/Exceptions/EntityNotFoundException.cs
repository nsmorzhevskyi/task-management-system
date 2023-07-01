namespace TaskManagementSystem.Domain.Exceptions;

public class EntityNotFoundException : DomainException
{
    private const string ExceptionMessage = "Requested entity was not found.";
    public EntityNotFoundException()
        : base(ExceptionMessage)
    {
        
    }
    
    public EntityNotFoundException(string message)
        : base(message)
    {
    }
}