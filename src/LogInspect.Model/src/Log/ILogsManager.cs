namespace LogInspect
{
    public interface ILogsManager
    {
        public List<Log>? TryLoadLogsFromFile();
    }
}