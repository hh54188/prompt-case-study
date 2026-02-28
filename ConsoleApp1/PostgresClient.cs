namespace ConsoleApp1;

public class PostgresClient : DatabaseClientBase
{
    public override async Task Query(string queryStatement)
    {
        LogMethodCalled();
    }
}
