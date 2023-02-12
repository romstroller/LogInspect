namespace LogInspect
{
    public interface IQueryManager
    {
        public List<string[]>? ExecuteQuery(Query query);
    }
}