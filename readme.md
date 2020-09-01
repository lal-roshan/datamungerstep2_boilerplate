## Seed code - Boilerplate for Step 2 - Database Engine Assignment

### Problem Statement

In our first assignment we extracted different parts of the query and **just displayed**. 

In this assignment we should store the query parameters/parts. We should think where to store these parameters.  

To do this we have to create a separate class with properties and methods.

The class name should be meaningful like **QueryParameter**. In this **QueryParameter** class we need to add properties so that we store various parts of 
the query string which we splitted in our earlier assignment.

## How to identify the properties of this QueryParameter?
It is based on what parts of the query string we displayed/printed in the previous assignment.

    i.e.,
    - queryString
    - file
    - baseQuery
    - restrictions
    - fields
    - QUERY_TYPE
    - where condition / filter
    - logicalOperators
    - aggregateFunctions
    - relational operators
    - order by field
    - group by field
    - aggregate fields

For all these properties we need to identify proper data type.  Let us decide now.
- QueryString-> string
- FileName -> string
- BaseQuery -> string
- Restrictions-> FilterCondition[]
- Fields -> string[]
- Query_Type -> string
- Fields ->  string
- LogicalOperators -> string[]  ( because we are fetching only logical operators.  There may be more than one logical operators in the given query string)
- AggregateFunctions -> AggregateFunction[]
- OrderByFields -> string[]  (if we need to sort based on only one field.)
- GroupByFields -> string[] (if we need to sort based on only one field.)

where condition/ filter part
---------------------------
 In previous assignment-I we have done the following (just printed on the console)
 
    Input String  : **select * from ipl.csv where season > 2014 and city ='Bangalore';**
    Output String : 
                    Condition 1 : 
                        variable : season
                        operator : > 
                        value    : 2014 
                    Condition 2 : 
                        variable : city
                        operator : =
                        value    : 'bangalore'
 
 These values should be captured in the proper data type.  We need to store variable, operator and value. To store/capture these 3 values we need a separate class.
 
 The class name can be Condition OR Restriction OR FilterCondition OR Criteria etc.,  
 
 Let us consider it as FilterCondition class
 
 In this class we need 3 parameters of String type.
 - propertyName : string
 - propertyValue : string
 - condition : string  ( Assume that all the values are String type.  Whenever required, we can convert into other datatype easily.)
 Also we need to add toString method and parameterized constructor
 
Aggregate Fields
------------------
In previous assignment-I we have done the following (just printed on the console)

User may require the information like who is getting maximum salary or minimum age etc., these are called aggregate functions (minimum, maximum, count, average, sum)

    Input String : **select avg(win_by_wickets),min(win_by_runs) from ipl.csv;** 
    Output String : 
            Aggregate 1
                Aggregate Name  : avg
                Aggregate Field : win_by_wickets

            Aggregate 2
                Aggregate Name  : min
                Aggregate Field : win_by_runs

These values should be captured in the proper data type.  We need to store **aggregate name, aggregate value**.
Totally we need 2 properties.
 
Create a separate class called **AggregateFunction** and add the following fields along with setter/getter methods also add toString method and parameterized constructor

Class : **AggregateFunction**
   
    Properties : 
        field : string
        function : string
        
    
The final QueryParameter class consist of
-----------------------------------------
    - queryString : string
    - file  : string
    - baseQuery -> string
    - fields -> string[]
    - QUERY_TYPE -> string
    - restrictions : Restriction[]   ( Array/group of restrictions.  A query may have multiple restrictions/filter conditions)
    - logicalOperators : string[]
    - aggregateFunctions -> AggregateFunction[]
    - orderByFields : string[]
    - groupByFields  :  string[]

Where we parse the query string build **QueryParameter** object? The parsing logic should not be in **QueryParameter** class. This class should consist of just properties.

The parsing should be done in separate class. The class name can be like **QueryParser**. In this class write a method which takes query string as input and return **QueryParameter** object.

The complete method signature looks like

    public QueryParameter parseQuery(string queryString)
    {

        //parse the query string
        //construct the QueryParameter

        //return Query Parameter object

}


### Expected solution
Return proper query parameter object

### Project structure

The folders and files you see in this repositories, is how it is expected to be in projects, which are submitted for automated evaluation by Hobbes

	Project
	|	
	├── DbEngine		//main project to implement all requirements
	|		└── AggregateFunction.cs          // This class is used to store Aggregate Function
	|		└── QueryParameter.cs             // This class contains the parameters/properties of QueryParameter
	|		└── QueryParser.cs                // This class will parse the queryString and return an object of QueryParameter class
	|		└── FilterCondition.cs                // This class is for storing Restriction object
	├── test         // all your test cases will be stored in this namespace/project
    |       └── DbEngineTest.cs  
	|
	├── .sln			                    // This is automatically generated by visual studio.
	|
	└── PROBLEM.md  		                    // This files describes the problem of the assignment/project, you can provide as much as information and clarification you want about the project in this file

> PS: All lint rule files are by default copied during the evaluation process, however if need to be customizing, you should copy from this repo and modify in your project repo


#### To use this as a boilerplate for your assignment, please follow these steps

1. **FORK** this repository in your Gitlab account

2. **CLONE** the forked repository, containing the boilerplate in your local machine
     
3. Navigate to  folder datamungerstep2_boilerplate

    `cd datamungerstep2_boilerplate`

4. Check the status of your repository
     
     `git status`

5. Complete the solution as per the instructions given in problem.md and the boilerplate code

6. Use the following command to update the index using the current content found in the working tree, to prepare the content staged for the next commit.

     `git add .`
 
7. Commit and Push the project to git

     `git commit -a -m "Initial commit | or place your comments according to your need"`

     `git push -u origin master`

8. Check on the git repo online, if the files have been pushed

9. Submit to [Hobbes](https://hobbes-ust.stackroute.in) for automated testing


### Important instructions for Participants
> - We expect you to write the assignment on your own by following through the guidelines, learning plan, and the practice exercises
> - The code must not be plagirized, the mentors will randomly pick the submissions and may ask you to explain the solution
> - The code must be properly indented, code structure maintained as per the boilerplate and properly commented
> - Follow through the problem statement shared with you
