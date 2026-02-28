namespace ConsoleApp1;

public class SQLServerClient : DatabaseClientBase
{
    public override async Task Query(string queryStatement)
    {
        LogMethodCalled();
    }
}
