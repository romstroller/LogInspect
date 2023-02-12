namespace LogInspect
{
    public class LogsManager : ILogsManager
    {
        ILogParser LogParser;
        public string LogPath;

        public LogsManager(
            string logPath,
            ILogParser logParser)
        {
            this.LogPath = logPath;
            this.LogParser = logParser;
        }
        public List<Log>? TryLoadLogsFromFile()
        {
            string[] logArray;
            try { logArray =  System.IO.File.ReadAllLines(LogPath); }
            catch ( FileNotFoundException ) {
                return null;
                }
            return LogParser.ParseArrayAsLogs(logArray);
        }
    }
}