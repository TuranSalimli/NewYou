namespace NewYou.Domain.Exceptions;

public class LoginFailedException : Exception
{
    public LoginFailedException(string message = "Giriş mümkün olmadı") : base(message) { }

    public LoginFailedException(Exception innerException, string message = "Giriş mümkün olmadı")
        : base(message, innerException) { }

}

