namespace NewYou.Domain.Exceptions;
public class RegisterFailedException : Exception
{
    public List<string> Errors { get; }

    public RegisterFailedException()
        : base("Register failed.")
    {
        Errors = new List<string>();
    }

    public RegisterFailedException(string message)
        : base(message)
    {
        Errors = new List<string>();
    }

    public RegisterFailedException(string message, Exception innerException)
        : base(message, innerException)
    {
        Errors = new List<string>();
    }

    public RegisterFailedException(List<string> errors)
        : base("Register failed due to validation errors.")
    {
        Errors = errors;
    }

    public RegisterFailedException(string message, List<string> errors)
        : base(message)
    {
        Errors = errors;
    }
}
