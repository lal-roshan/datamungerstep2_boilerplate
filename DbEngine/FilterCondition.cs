namespace DbEngine
{
    /*
 * This class is used for storing name of field, condition and value for 
 * each conditions
 * generate properties for this class,
 * Also override toString method
 * */
    public class FilterCondition
    {
        public string propertyName;

        public string propertyValue;

        public string condition;

        // Write logic for constructor
        public FilterCondition(string propertyName, string propertyValue, string condition)
        {
            this.propertyName = propertyName;
            this.propertyValue = propertyValue;
            this.condition = condition;
        }

    }
}
