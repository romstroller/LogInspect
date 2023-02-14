namespace LogInspect
{
    public class ValidationData
    {
        public static Dictionary<string, Func<string, bool> > Actions = 
            new Dictionary<string, Func<string, bool> >
        {
            { "count", str => str.Length > 0 },
            { "list", str => str.Length > 0 },
            { "help", str => new string[] { "features", "guide" }.Contains( str ) }
        };
        public static Dictionary<string, Func<string, bool> > Modifiers = 
            new Dictionary<string, Func<string, bool> >
        {
            { "where", str => str.Split( '=' ).Length == 2 },
            { "limit", str => Int32.TryParse( str, out int value ) }
        };
        public static readonly List<string[]> Guide = new List<string[]>
        {
            new string[] { "A query takes the form of an ACTION, a FEATURE and optional MODIFIERS", },
            new string[] { "    in the form of action:feature~modifier:statement ", },
            new string[] { "", },
            new string[] { "ACTIONS:", },
            new string[] { "  list    - get all values for a feature, eg list:url", },
            new string[] { "  count   - count different values for a feature, eg count:timezone", },
            new string[] { "  help    - help:features for available features, or this guide help:guide", },
            new string[] { "", },
            new string[] { "MODIFIERS can be appended with '~' separator", },
            new string[] { "  where   - set equality condition '=' on another feature", },
            new string[] { "              eg. list:useragent~where:protocol=HTTP/1.1", },
            new string[] { "              or  list:clientip~where:userid=admin", },
            new string[] { "  limit   - show only the first n. results (eg. count:useragent~limit:10 )" },
            };
    }
}
