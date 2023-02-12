namespace LogInspect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConsoleMessenger consoleMessenger = new ConsoleMessenger();
            IInputValidator inputValidator = new InputValidator( consoleMessenger );
            ILogParser logParser = new LogParser();
            ILogsManager logsManager = new LogsManager( "example-data.log", logParser );
            
            IQueryManager queryManager = new QueryManager( logsManager );

            IQueryParser queryParser = new QueryParser();
            IConsoleWorker consoleWorker = new ConsoleWorker(
                inputValidator,
                consoleMessenger,
                queryParser,
                queryManager
            );
            
            // Top 5 visited urls
            List<string[]>? top5Urls = queryManager.ExecuteQuery( 
                new CountQuery( "url" )  { Limit = 5 } );
            
            // Top 5 request types (i.e. GET, POST)
            List<string[]>? top5methods = queryManager.ExecuteQuery( 
                new CountQuery( "method" )  { Limit = 5 } );
            
            // List Urls returning a 404
            List<string[]>? urlWhereStatus404 = queryManager.ExecuteQuery( 
                new ListQuery( "url" ) { Where = new List<string[]>{ new string[]{ "status", "404" } } });
            
            consoleMessenger.OutputResults( top5Urls, "Top 5 visited urls" );
            consoleMessenger.OutputResults( top5methods, "Top 5 request types (i.e. GET, POST)" );
            consoleMessenger.OutputResults( urlWhereStatus404, "List Urls returning a 404" );
            
            consoleWorker.StartFromArgs(args);
        }
    }
}