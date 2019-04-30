using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestApi.Helper
{
    public class Validate
    {
        public static void TableName(string paramValue, string paramName)
        {
            Null(paramValue, paramName);

            var regex = new Regex("^[A-Za-z][A-Za-z0-9]{2,62}$", RegexOptions.Compiled);
            if(!regex.IsMatch(paramName))
            {
                throw new ArgumentException("Table names must conform to these rules: " +
                    "May contain only alphanumeric characters. " +
                    "Cannot begin with a numeric character. " +
                    "Are case-insensitive. " +
                    "Must be from 3 to 63 characters long.",paramName??"");
            }
        }

        public static void Null(object paramValue, string paramName)
        {
            if(paramValue == null)
            {
                throw new ArgumentException("Parameter must not be null.",paramName??"");
            }
        }
    }
}
