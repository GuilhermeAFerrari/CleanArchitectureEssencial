namespace CleanArchMvc.Domain.Validation;

public class DomainExceptionValidation : Exception
{
    public DomainExceptionValidation(string message) : base(message)
    {                
    }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainExceptionValidation(error);
    }
}
