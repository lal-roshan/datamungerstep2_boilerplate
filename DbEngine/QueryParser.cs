#region Namespace
using DbEngine.Helper;
using System.Collections.Generic;
using System.Linq;

namespace DbEngine
{
    #region Class
    public class QueryParser
    {
        private QueryParameter queryParameter;
        public QueryParser()
        {
            queryParameter = new QueryParameter();
        }

        private void SetQueryParameters(string queryString)
        {
            queryParameter.QueryString = queryString;
            queryParameter.FileName = QueryHelper.GetFileNameFromQuery(queryParameter.QueryString);
            queryParameter.Fields = GetFields(queryParameter.QueryString);
            queryParameter.Restrictions = GetRestrictions();
            queryParameter.LogicalOperators = GetLogicalOperators();
            queryParameter.AggregateFunctions = GetAggregateFunctions(queryParameter.QueryString);
            queryParameter.GroupByFields = GetGroupByFields(queryParameter.QueryString);
            queryParameter.OrderByFields = GetOrderByFields(queryParameter.QueryString);
        }

        /*
	 * this method will parse the queryString and will return the object of
	 * QueryParameter class
	 */

        public QueryParameter parseQuery(string queryString)
        {
            if (queryParameter == null)
            {
                queryParameter = new QueryParameter();
            }
            SetQueryParameters(queryString);
            return queryParameter;
        }

        /*
	 * extract the selected fields from the query string. Please note that we will
	 * need to extract the field(s) after "select" clause followed by a space from
	 * the query string. For eg: select city,win_by_runs from data/ipl.csv from the
	 * query mentioned above, we need to extract "city" and "win_by_runs". Please
	 * note that we might have a field containing name "from_date" or "from_hrs".
	 * Hence, consider this while parsing.
	 */
        private string[] GetFields(string queryString)
        {
            return QueryHelper.GetSelectedFields(queryString)?.ToArray();
        }

        /*
	 * extract the conditions from the query string(if exists). for each condition,
	 * we need to capture the following: 1. Name of field 2. condition 3. value
	 * 
	 * For eg: select city,winner,team1,team2,player_of_match from data/ipl.csv
	 * where season >= 2008 or toss_decision != bat
	 * 
	 * here, for the first condition, "season>=2008" we need to capture: 1. Name of
	 * field: season 2. condition: >= 3. value: 2008 Also use trim() where ever
	 * required
	 * 
	 * the query might contain multiple conditions separated by OR/AND operators.
	 * Please consider this while parsing the conditions .
	 * 
	 */

        private FilterCondition[] GetRestrictions()
        {
            if (queryParameter != null && !string.IsNullOrEmpty(queryParameter.QueryString.Trim()))
            {
                List<string> restrictionsList = QueryHelper.GetConditionInFilter(queryParameter.QueryString);

                if (restrictionsList != null &&
                   !string.Equals(restrictionsList.First(), Common.NoFilterString))
                {
                    List<FilterCondition> filters = new List<FilterCondition>();
                    foreach (string restriction in restrictionsList)
                    {
                        List<string> parts = Common.SplitConditionWords(restriction);
                        if (parts != null && parts.Count == 3)
                        {
                            filters.Add(new FilterCondition(propertyName: parts[0],
                                        propertyValue: parts[2], condition: parts[1]));
                        }
                    }
                    if (filters.Any())
                    {
                        return filters.ToArray();
                    }
                }

            }
            return null;
        }

        /*
	 * extract the logical operators(AND/OR) from the query, if at all it is
	 * present. For eg: select city,winner,team1,team2,player_of_match from
	 * data/ipl.csv where season >= 2008 or toss_decision != bat and city =
	 * bangalore
	 * 
	 * the query mentioned above in the example should return a List of Strings
	 * containing [or,and]
	 */

        private string[] GetLogicalOperators()
        {
            if (queryParameter != null && !string.IsNullOrEmpty(queryParameter.QueryString.Trim()))
            {
                return QueryHelper.GetLogicalOperators(queryParameter.QueryString)?.ToArray();
            }
            return null;
        }

        /*
             * extract the aggregate functions from the query. The presence of the aggregate
             * functions can determined if we have either "min" or "max" or "sum" or "count"
             * or "avg" followed by opening braces"(" after "select" clause in the query
             * string. in case it is present, then we will have to extract the same. For
             * each aggregate functions, we need to know the following: 1. type of aggregate
             * function(min/max/count/sum/avg) 2. field on which the aggregate function is
             * being applied
             * 
             * Please note that more than one aggregate function can be present in a query
             * 
             * 
             */
        private AggregateFunction[] GetAggregateFunctions(string queryString)
        {
            List<string> aggregateStrings = QueryHelper.GetAggregateFunctions(queryString);
            if (aggregateStrings != null &&
                !string.Equals(aggregateStrings.First(), Common.NoAggregateFunctions))
            {
                List<AggregateFunction> aggregates = new List<AggregateFunction>();
                foreach (string aggregate in aggregateStrings)
                {
                    List<string> parts = Common.SplitAggregateFields(aggregate);
                    if (parts != null && parts.Count == 2)
                    {
                        aggregates.Add(new AggregateFunction(field: parts[0], function: parts[1]));
                    }
                }
                if (aggregates.Any())
                {
                    return aggregates.ToArray();
                }
            }
            return null;
        }

        /*
	 * extract the order by fields from the query string. Please note that we will
	 * need to extract the field(s) after "order by" clause in the query, if at all
	 * the order by clause exists. For eg: select city,winner,team1,team2 from
	 * data/ipl.csv order by city from the query mentioned above, we need to extract
	 * "city". Please note that we can have more than one order by fields.
	 */
        private string[] GetOrderByFields(string queryString)
        {
            return QueryHelper.GetOrderField(queryString)?.ToArray();
        }

        /*
	 * extract the group by fields from the query string. Please note that we will
	 * need to extract the field(s) after "group by" clause in the query, if at all
	 * the group by clause exists. For eg: select city,max(win_by_runs) from
	 * data/ipl.csv group by city from the query mentioned above, we need to extract
	 * "city". Please note that we can have more than one group by fields.
	 */
        private string[] GetGroupByFields(string queryString)
        {
            return QueryHelper.GetGroupByField(queryString)?.ToArray(); ;
        }
    }
    #endregion
}

#endregion