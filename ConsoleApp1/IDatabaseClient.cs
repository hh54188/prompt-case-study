namespace ConsoleApp1;

public interface IDatabaseClient
{
    Task Query(string queryStatement);
    Task CloseConnection();
}