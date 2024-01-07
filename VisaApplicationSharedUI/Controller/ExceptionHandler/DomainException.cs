namespace VisaApplicationSharedUI.Controller.ExceptionHandler;
public abstract class DomainException : Exception
{
    public DomainException(string message) : base(message)
    { }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message)
    { }
}

public class InternalServerException : DomainException
{
    public InternalServerException(string message) : base(message)
    { }
}

public class BadRequestException : DomainException
{
    public BadRequestException(string message) : base(message)
    { }
}

public class UnAuthorizedException : DomainException
{
    public UnAuthorizedException(string message) : base(message)
    { }
}
