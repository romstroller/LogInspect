namespace LogInspect
{
    public class QueryManager : IQueryManager
    {
        public ILogsManager LogsManager;
        public QueryManager(ILogsManager logsManager)
        {
            this.LogsManager = logsManager;
        }
        public List<string[]>? ExecuteQuery(Query query)
        {
            List<Log>? logs = LogsManager.TryLoadLogsFromFile();
            if (logs == null) { return null; }
            
            switch (query)
            {
                // count must precede list as is subtype
                case CountQuery: return GetCount((CountQuery)query, logs);
                case ListQuery: return GetList((ListQuery)query, logs);
                case HelpQuery: return GetHelp((HelpQuery)query);
                default: return null;
            }
        }
        List<string> GetLogFeatures()
        {
            return typeof(Log)
                .GetProperties()
                .Select(propInfo => propInfo.Name)
                .ToList();
        }
        List<Log> ApplyWhereStatements( List<string[]> whereStatements, List<Log> logs )
        {
            foreach ( string[] whereStatement in whereStatements )
            {
                string whereFeature = whereStatement[0];
                string queryValue = whereStatement[1];
                
                System.Reflection.PropertyInfo? wherePropInfo = typeof(Log)
                    .GetProperty( whereFeature );
                
                // no matching property for "where" feature, ignore (add warning)
                if ( wherePropInfo == null ) {  continue;  }
                
                // if valueObj not null, get tuple of log and its valueObj for prop
                List<(Log, object)> whereValueObjs = (from log in logs
                    where wherePropInfo.GetValue( log ) != null
                    select ( log, wherePropInfo.GetValue( log ))).ToList();
                
                // take log if property value is match to query-value
                logs = (from tup in whereValueObjs
                    where tup.Item2.ToString() == queryValue
                    select tup.Item1).ToList();
            }
            return logs;
        }
        List<string[]>? GetStringsFromQueryProperty( Query query, List<Log> logs )
        {
            System.Reflection.PropertyInfo? propInfo = typeof(Log)
                .GetProperty( query.Value );

            if (propInfo == null) { return null; }  // query ft was not a valid property
            
            List<object> featureValueObjs = (from log in logs
                where propInfo.GetValue( log ) != null
                select propInfo.GetValue( log )).ToList();
            
            List<string> values = (from obj in featureValueObjs
                    select obj.ToString() ).ToList();
            
            List<string[]> results = values.Select( val => new string[]{ val }).ToList();
            return results;
        }
        public List<string[]>? GetList(ListQuery query, List<Log> logs, bool ignoreLimit = false)
        {
            if ( query.Where.Count > 0 ) { logs = ApplyWhereStatements( query.Where, logs );}
            List<string[]>? results = GetStringsFromQueryProperty( query, logs );
            if ( results == null ){ return null; }
            if ( query.Limit != null )
            {
                if ( ignoreLimit == false ) results = ApplyLimit( query.Limit.Value, results );
            }
            return results;
        }
        List<string[]> HandleNull( List<string?[]> resultsNbl )
        {
            List<string[]> results = new List<string[]>();
            string[] nullResult = new string[]{ "null" };
            foreach ( string?[] arr in resultsNbl )
            {
                if ( arr == null ){ results.Add( nullResult ); }
                else 
                {
                    List<string> result = new List<string>();
                    foreach( string? str in arr )
                    {
                        if ( str != null ) { result.Add( str ); }
                        else { result.Add( "null" ); }
                    }
                    results.Add( result.ToArray() );
                }
            }
            return results;
        }
        public List<string[]> ApplyLimit( int limit, List<string[]> results )
        {
            return results.Take( limit ).ToList();
        }
        public List<string[]>? GetCount(CountQuery query, List<Log> logs)
        {
            List<string[]>? featureList = GetList(query, logs, ignoreLimit:true );
            if (featureList == null) { return null; }

            List<string?[]> resultsNbl = featureList.GroupBy(arr => arr[0] )
                .Select(group => (new string?[]{
                    group.Key.ToString(), group.Count().ToString() }))
                .OrderByDescending(arr => arr[1])
                .ToList();
            
            List<string[]> results =  HandleNull( resultsNbl );
            if ( query.Limit != null ){ results =  ApplyLimit( query.Limit.Value, results ); }
            return results;
        }
        private List<string[]>? GetHelp(HelpQuery query)
        {
            List<string[]>? results = null;
            switch (query.Value)
            {
                case "guide":
                    results = ValidationData.Guide;
                    break;
                case "features":
                    results = GetLogFeatures()
                    .Select(feature => new string[] { feature })
                    .ToList();
                    break;
            }
            return results;
        }
    }
}