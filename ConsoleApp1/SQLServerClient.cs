using System.Reflection;

namespace ConsoleApp1;

public class SQLServerClient : DatabaseClientBase
{
    public override async Task Query(string queryStatement)
    {
        LogMethodCalled(MethodBase.GetCurrentMethod()?.Name);
        // 子类在此处编写实际的 Query 逻辑
    }

    public override async Task CloseConnection()
    {
        LogMethodCalled(MethodBase.GetCurrentMethod()?.Name);
        // 子类在此处编写实际的 CloseConnection 逻辑
    }
}
