using System.Runtime.CompilerServices;

namespace ConsoleApp1;

public abstract class DatabaseClientBase : IDatabaseClient
{
    private string ClientName => GetType().Name;

    protected void LogMethodCalled([CallerMemberName] string? methodName = null) =>
        Console.WriteLine($"{ClientName} {methodName} method called");

    public abstract Task Query(string queryStatement);
}
