namespace LogInspect
{
    public abstract class Query
    {
        public abstract string Value { get; }
    }
    public class HelpQuery : Query
    {
        public override string Value { get; }
        public HelpQuery( string topic )
        {
            this.Value = topic;
        }
    }
    public class ListQuery : Query
    {
        public override string Value { get; }
        public int? Limit { get; set; }
        public List<string[]> Where { get; set; } = new List<string[]>();
        
        public ListQuery( string feature, int? limit = null )
        {
            this.Value = feature;
            this.Limit = limit;
        }
    }
    public class CountQuery : ListQuery
    {
        public CountQuery(  
            string feature, int? limit = null ) 
            :base( feature, limit ){}
    }   
}