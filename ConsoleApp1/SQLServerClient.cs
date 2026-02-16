namespace ConsoleApp1;

public class SQLServerClient: IDatabaseClient
{
    public async Task Query(string queryStatement)
    {
        Console.WriteLine("SQLServerClient DB Query Completed");
    }
}