namespace DbEngine
{
    /* 
 * This class will contain the elements of the parsed Query String such as conditions,
 * logical operators,aggregate functions, file name, fields group by fields, order by
 * fields, Query Type
 * */
    public class QueryParameter
    {
        public string QueryString { get; set; }
        public string[] LogicalOperators { get; set; }
        public string FileName { get; set; }
        public string BaseQuery { get; set; }
        public string Query_Type { get; set; }
        public string[] Fields { get; set; }
        public string[] GroupByFields { get; set; }
        public string[] OrderByFields { get; set; }
        public FilterCondition[] Restrictions { get; set; }
        public AggregateFunction[] AggregateFunctions { get; set; }
    }
}
