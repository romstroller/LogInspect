namespace LogInspect
{
    public class Log
    {
        public string Input;
        public string? clientip { get; set; } = null;
        public string? clientid { get; set; } = null;
        public string? userid { get; set; } = null;
        public string? datetime { get; set; } = null;
        public string? timezone { get; set; } = null;
        public string? method { get; set; } = null;
        public string? protocol { get; set; } = null;
        public string? url { get; set; } = null;
        public int? status { get; set; } = null;
        public int? bytes { get; set; } = null;
        public string? unknown { get; set; } = null;
        public string? useragent { get; set; } = null;
        public Log( 
            string input
            )
        {
            this.Input = input;
        }
    }
}