namespace LogInspect
{
    public class ConsoleMessenger : IConsoleMessenger
    {
        
        public ConsoleMessenger( ){ }
        public void PrintBreak() { Console.WriteLine( $"\n{ new string('=', 79 ) }\n" ); }
        public void OutputResults( List<string[]>? results, string? description = null )
        {            
            PrintBreak();
            if ( description != null ) Console.WriteLine( $"{description}\n" );
            if ( results == null ) { Console.WriteLine( "Results were null" ); }
            else {
                foreach ( string[] result in results )
                {
                    Console.WriteLine( string.Join( " | ", result ) );
                }
            }
            PrintBreak();
        }
        public string? PromptInput()
        {
            Console.WriteLine( "Provide log query, 'help:guide' for query guide or leave blank to exit" );
            return Console.ReadLine();
        }
        public void WarnBadSyntax(string commandSegment)
        {
            Console.WriteLine( $"Input was not in form of [ ACTION:VALUE ]: [ {commandSegment} ]" );
        }
        public void WarnNoLogs( string logPath )
        {
            Console.WriteLine( $"No log file located at [ {logPath} ] - please check" );
        }
        public void WarnCommandInvalid( string commandSegment )
        {
            Console.WriteLine( $"[ {commandSegment} ] is not a valid command" );
        }
        public void WarnQueryFailure(string input)
        {
            Console.WriteLine( $"Query from input [ {input} ] was unsuccessful" );
        }
    }
}