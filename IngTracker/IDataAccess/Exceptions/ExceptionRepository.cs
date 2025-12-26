namespace IDataAccess.Exceptions;

public class ExceptionRepository : Exception
{
    public ExceptionRepository(string mensaje) : base(mensaje)
    {
    }
}
