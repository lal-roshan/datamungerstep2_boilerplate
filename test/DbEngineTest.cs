using System;
using DbEngine;
using Xunit;
using FluentAssertions;

namespace test
{
    public class DbEngineTest:IClassFixture<TestFixture>
    {
        TestFixture _fixture;
        private QueryParser _parser;
        private QueryParameter queryParameter;
        private string queryString;
        public DbEngineTest(TestFixture fixture)
        {
            _fixture = fixture;
            _parser = _fixture.parser;
        }

        [Fact]
        public void TestGetFileName()
        {
            //Arrange
            queryString = "select * from ipl.csv";
            string expected = "ipl.csv";

            //Act
            queryParameter = _parser.parseQuery(queryString);
            
            //Assert
            queryParameter.FileName.Should().BeEquivalentTo(expected, "because we passed 'select * from ipl.csv' and file name can be found after from clause ");
        }

        [Fact]
        public void TestGetFieldNames()
        {
            //Arrange
            string query = "select city,winner,player_match from ipl.csv where season > 2014";

            //Act
            queryParameter = _parser.parseQuery(query);
            string[] expected = new string[] { "city", "winner", "player_match" };
            //Assert
            queryParameter.Fields.Should().NotBeNull("because we passed field names city,winner,player_match in our query");
            queryParameter.Fields.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TestGetRestrictions()
        {
            queryString = "select city,winner,player_match from ipl.csv where season > 2014";
            queryParameter = _parser.parseQuery(queryString);

            FilterCondition[] conditions = new FilterCondition[] { new FilterCondition("season", "2014", ">") };

            queryParameter.Restrictions.Should().BeEquivalentTo(conditions,"because we passed the query with condition season > 2014");
        }

        [Fact]
        public void TestLogicalAnd()
        {
            queryString = "select city,winner,player_match from ipl.csv where season > 2014 and city ='Bangalore'";
            queryParameter = _parser.parseQuery(queryString);

            string[] logicalop = new string[] {"and" };
            queryParameter.LogicalOperators.Should().BeEquivalentTo(logicalop,"because we passes the query with two conditions using and operator");
        }

        [Fact]
        public void TestLogicalOr()
        {
            queryString = "select city,winner,player_match from ipl.csv where season > 2014 or city ='Bangalore'";
            queryParameter = _parser.parseQuery(queryString);

            string[] logicalop = new string[] {"or" };
            queryParameter.LogicalOperators.Should().BeEquivalentTo(logicalop, "because we passes the query with two conditions using or operator");
        }

        [Fact]
        public void TestLogicalAnd_Or()
        {
            queryString = "select city,winner,player_match from ipl.csv where season > 2014 and city ='Bangalore' or city ='Delhi'";
            queryParameter = _parser.parseQuery(queryString);

            string[] logicalop = new string[] {"or","and" };
            queryParameter.LogicalOperators.Should().BeEquivalentTo(logicalop, "because we passes the query with two conditions using or operator");
        }

        [Fact]
        public void TestAggregateFunctions()
        {
            queryString = "select count(city),avg(win_by_runs),min(season),max(win_by_wickets) from ipl.csv";
            queryParameter = _parser.parseQuery(queryString);

            AggregateFunction[] aggregateFunctionList = new AggregateFunction[] { 
            new AggregateFunction("city", "count"),
            new AggregateFunction("win_by_runs", "avg"),
            new AggregateFunction("season", "min"),
            new AggregateFunction("win_by_wickets", "max")};

            queryParameter.AggregateFunctions.Should().BeEquivalentTo(aggregateFunctionList,"because we passed query with avg,max,min and count");
        }

        [Fact]
        public void TestGroupByClause()
        {
            queryString = "select city,winner,player_match from ipl.csv group by city";
            queryParameter = _parser.parseQuery(queryString);

            string[] groupByFields = new string[]{ "city"};
            queryParameter.GroupByFields.Should().BeEquivalentTo(groupByFields, "because we passed the query with group by city");
        }

        [Fact]
        public void TestOrderByClause()
        {
            queryString = "select city,winner,player_match from ipl.csv order by city";
            queryParameter = _parser.parseQuery(queryString);

            string[] orderByFields = new string[] { "city" };
            queryParameter.OrderByFields.Should().BeEquivalentTo(orderByFields, "because we passed the query with order by city");
        }

        [Fact]
        public void TestGroupByOrderByClause()
        {
            queryString = "select city,winner,player_match from ipl.csv where season > 2014 "
                + "and city ='Bangalore' group by winner order by city";
            queryParameter = _parser.parseQuery(queryString);

            string[] orderByFields = new string[] { "city"};

            string[] groupByFields = new string[]{ "winner" };

            queryParameter.GroupByFields.Should().BeEquivalentTo(groupByFields, "because we passed the query with group by winner");
            queryParameter.OrderByFields.Should().BeEquivalentTo(orderByFields, "because we passed the query with order by city");
        }


    }

    public class TestFixture : IDisposable
    {
        public QueryParser parser { get; private set; }
        public TestFixture()
        {
            parser = new QueryParser();
        }
        public void Dispose()
        {
            parser = null;
        }
    }
}
