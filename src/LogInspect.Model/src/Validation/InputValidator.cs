namespace LogInspect
{
    public class InputValidator : IInputValidator
    {
        IConsoleMessenger ConsoleMessenger;
        public InputValidator( IConsoleMessenger consoleMessenger )
        {
            this.ConsoleMessenger = consoleMessenger;
        }
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
            string[] command = commandSegment.Split(":");
            if (command.Length != 2)
            {
                ConsoleMessenger.WarnBadSyntax( commandSegment );
                return false; 
            }
            string commandInstruction = command[0];
            string commandValue = command[1];
            bool isValidValue = true;
            var commandSet = isAction ? ValidationData.Actions : ValidationData.Modifiers;
            
            try { isValidValue = commandSet[ commandInstruction ]( commandValue ); }
            catch ( KeyNotFoundException )
            {
                ConsoleMessenger.WarnCommandInvalid( commandSegment );
                return false;
            }
            return isValidValue;
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