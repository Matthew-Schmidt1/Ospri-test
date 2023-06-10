namespace OspriTest.Interfaces
{
    internal interface IDatabaseSettings
    {
        string DatabaseName { get; }
        string Password { get; }
        string Server { get; }
        bool UseInMemory { get; }
        string UserName { get; }

        string BuildConnectionString();
    }
}