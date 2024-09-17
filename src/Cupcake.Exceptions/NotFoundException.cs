namespace Cupcake.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(Type type) : base(type.Name + " not found.") { }

    public NotFoundException(string message) : base(message) { }
}
