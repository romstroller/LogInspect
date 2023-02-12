namespace LogInspect
{
    public interface ILogParser
    {
        public List<Log>? ParseArrayAsLogs(string[] logfileInput);
    }
}