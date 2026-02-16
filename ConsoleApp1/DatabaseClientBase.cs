using System.Reflection;

namespace ConsoleApp1;

public abstract class DatabaseClientBase : IDatabaseClient
{
    private string ClientName => GetType().Name;

    /// <summary>
    /// 供子类在各自方法内调用，记录「类名 + 当前方法名」的日志，无需手写类名或方法名。
    /// </summary>
    protected void LogMethodCalled(string? methodName) =>
        Console.WriteLine($"{ClientName} {methodName} method called");

    public abstract Task Query(string queryStatement);
    public abstract Task CloseConnection();
}
