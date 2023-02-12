namespace LogInspect
{
    public class ConsoleWorker : IConsoleWorker
    {
        public IInputValidator InputValidator;
        public IConsoleMessenger ConsoleMessenger;
        public IQueryParser QueryParser;
        public IQueryManager QueryManager;
        public ConsoleWorker(
            IInputValidator inputValidator,
            IConsoleMessenger consoleMessenger,
            IQueryParser queryParser,
            IQueryManager queryManager
            )
        {
            this.InputValidator = inputValidator;
            this.ConsoleMessenger = consoleMessenger;
            this.QueryParser = queryParser;
            this.QueryManager = queryManager;
        }
        public void StartFromArgs(string[] args)
        {
            string? input = string.Join( " ", args );
            if ( false == InputValidator.CheckInputIsQuerySyntax( input.ToLower() ) ) 
            {
                input = ConsoleMessenger.PromptInput(); 
            }
            RunPromptLoop( input );
        }
        public void RunPromptLoop(string? input )
        {
            while( false == string.IsNullOrWhiteSpace( input ) )
            {
                input = input.ToLower();
                if ( InputValidator.CheckInputIsQuerySyntax( input ))
                {
                    Query? query = QueryParser.TryParseQuery( input );
                    if ( query == null )
                    { 
                        ConsoleMessenger.WarnBadSyntax( input); 
                        return;
                    }
                    List<string[]>? results = QueryManager.ExecuteQuery( query );
                    if ( results == null ){ ConsoleMessenger.WarnQueryFailure( input ); }
                    else { ConsoleMessenger.OutputResults( results ); }
                }
                input = ConsoleMessenger.PromptInput();
            }
        }
    }
}