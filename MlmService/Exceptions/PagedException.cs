namespace MlmService.Exceptions;

public class PagedException : Exception
{
    public PagedException(string message) : base(message) { }

    public PagedException() : base()
    {
    }

    public PagedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}