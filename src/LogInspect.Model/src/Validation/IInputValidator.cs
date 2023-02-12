namespace LogInspect
{
    public interface IInputValidator
    {
        public bool CheckInputIsQuerySyntax( string? input );
    }
    
}