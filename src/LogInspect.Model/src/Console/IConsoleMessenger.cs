namespace LogInspect
{
    public interface IConsoleMessenger
    {
        public void OutputResults( List<string[]>? results, string? description = null );
        public string? PromptInput();
        public void WarnBadSyntax(string actionSegment);
        public void WarnNoLogs( string logPath );
        public void WarnCommandInvalid( string actionSegment );
        public void WarnQueryFailure( string input );
    }
}