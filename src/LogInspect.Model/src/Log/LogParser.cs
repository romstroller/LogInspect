namespace LogInspect
{
    public class LogParser : ILogParser
    {
        public Log? ParseLog(string line)
        {
            Log newLog = new Log(line);
            string[] segments = line.Split(' ');
            if (segments.Length < 12) { return null; }

            newLog.clientip = segments[0];
            newLog.clientid = segments[1];
            newLog.userid = segments[2];
            newLog.datetime = segments[3][1..];
            newLog.timezone = segments[4][..^1];
            newLog.method = segments[5][1..];
            newLog.url = segments[6];
            newLog.protocol = segments[7][..^1];
            newLog.status = Int32.Parse(segments[8]);
            newLog.bytes = Int32.Parse(segments[9]);
            newLog.unknown = segments[10];
            newLog.useragent = string.Join(" ", segments[11..]);

            return newLog;
        }
        public List<Log>? ParseArrayAsLogs(string[] logArray)
        {
            List<Log> logs = new List<Log>();
            foreach (string line in logArray)
            {
                Log? parsedLog = ParseLog(line);
                if (parsedLog != null) { logs.Add(parsedLog); }
            }
            if (logs.Count > 0) { return logs; }
            return null;
        }
    }
}