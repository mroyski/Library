using System;
using System.Reflection;
using Library.Api.Models;

namespace Library.Api.Helpers
{
    public static class ParameterHelper
    {
        public static string FormatParameter<T>(string input)
        {
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(Book).GetProperties();


            // var b = typeof(T).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var name = propertyInfos[i].Name.ToString().ToLower();
                if (input.ToLower() == name)
                {
                    var propToReturn = propertyInfos[i].ToString().Split(' ')[1];
                    return propToReturn;
                }
                    
            }
            throw new Exception("Property does not exist.");

            // var property = typeof(Book).GetProperty(formattedParameter)
        }
    }
}
