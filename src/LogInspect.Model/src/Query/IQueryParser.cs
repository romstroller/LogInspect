namespace LogInspect
{
    public interface IQueryParser
    {
        public Query? TryParseQuery(string input);
    }

}