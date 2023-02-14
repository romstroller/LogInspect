namespace LogInspect
{
    public class InputValidator : IInputValidator
    {
        public bool CheckInputIsQuerySyntax(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) { return false; }
            if (input.Length == 0) { return false; }
            string[] commandSegments = input.Split( "~" );
            if ( false == CheckCommandSyntaxValid( commandSegments[0], isAction:true ) )
            {
                return false; 
            }
            bool modifierSyntaxValid = true;
            if ( commandSegments.Length > 1 )
            {
                modifierSyntaxValid = CheckModifierSyntaxValid( commandSegments[1..] );
            }
            if ( modifierSyntaxValid == false ) { return false; }
            return true;
        }
        private bool CheckCommandSyntaxValid(string commandSegment, bool isAction=false )
        {
            string[] commandSegments = commandSegment.Split(":");
            if (commandSegments.Length != 2) {return false; }
            var commandSet = isAction ? ValidationData.Actions : ValidationData.Modifiers;
            try { return commandSet[ commandSegments[0] ]( commandSegments[1] ); }
            catch ( KeyNotFoundException ) { return false; }
        }
        private bool CheckModifierSyntaxValid(string[] modifierSegments)
        {
            foreach ( string modifierSegment in modifierSegments )
            {
                if ( false == CheckCommandSyntaxValid( modifierSegment ) ) { return false; }
            }
            return true;
        }
    }
}