namespace LogInspect
{
    public class QueryParser : IQueryParser
    {
        public Query? TryParseQuery(string input)
        {
            Query? newQuery = null;
            string[] commandSegments = input.Split( "~" );
            string[] actionSegment = commandSegments[0].Split( ":" );
            string actionCommand = actionSegment[0].Trim().ToLower();
            string actionValue = actionSegment[1].Trim().ToLower();
            switch ( actionCommand )
            {
                case "list": 
                    newQuery = new ListQuery( actionValue );
                    break;
                case "count": 
                    newQuery = new CountQuery( actionValue );
                    break;
                case "help": 
                    newQuery = new HelpQuery( actionValue );
                    break;
                default: return null;
            }
            if ( commandSegments.Length > 1 )
            {
                newQuery = ParseQueryModifiers( newQuery, commandSegments[1..] );
            }
            return newQuery;
        }
        public Query ParseQueryModifiers(Query newQuery, string[] modifierSegments )
        {
            foreach ( string modifierSegment in modifierSegments )
            {
                string[] modSegmentSplit = modifierSegment.Split( ":" );
                string modifierCommand = modSegmentSplit[0].Trim().ToLower();
                string modifierValue = modSegmentSplit[1].Trim().ToLower();
                
                switch ( newQuery )
                {
                    case ListQuery: // includes derived Count query
                        ListQuery listQuery = (ListQuery)newQuery;
                        switch ( modifierCommand )
                        {
                            case "where":
                                listQuery.Where.Add( modifierValue.Split( "=" ) );
                                break;
                            case "limit":
                                listQuery.Limit = Int32.Parse( modifierValue );
                                break;
                            default: throw new Exception( 
                                $"Query parse received valid unknown: {modifierCommand} {modifierValue}" );
                        }
                        break;
                    default: break; 
                }
            }
            return newQuery;
        }
    }
}