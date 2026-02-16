namespace ConsoleApp1;

public class PostgresClient: IDatabaseClient
{
    public async Task Query(string queryStatement)
    {
        Console.WriteLine("Postgres DB Query Completed");
    }
}