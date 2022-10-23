namespace CodeChef
{
    public static class Jsonconvert
    {

        public static string SerializeThisObject(object? value)
        {
            string result = "";

             List<object> obj = new List<object>();

            if (value != null)
            {
                obj.Add(value);

                foreach (object c in obj)
                {
                    string[] propertyNames = c.GetType().GetProperties().Select(p => p.Name).ToArray();
                    string firstElement = propertyNames.First();
                    string lastElement = propertyNames.Last();
                    int propertyNameslength = propertyNames.Length;

                    
                    foreach ( var prop in propertyNames)
                    {
                        
                        object propValue = c.GetType().GetProperty(prop).GetValue(c, null);

                        Type propValueType = propValue.GetType();

                       
                        if (propertyNameslength == 1)
                        {
                            if (propValueType.Name == "Int32" || propValueType.Name == "Boolean")
                                result = "{"+ "\"" + $"{prop}" + "\"" + ":"+ $"{propValue}" + "}";                           
                            else
                                result = "{" + "\"" + $"{prop}" +"\"" + ":" + "\"" +$"{propValue}" +"\"" + "}";
                        }                         
                        else
                        {
                            if (firstElement == prop)
                            {
                                if (propValueType.Name == "Int32" || propValueType.Name == "Boolean")
                                    result += "{" + "\"" + $"{prop}" + "\""+ ":" + $"{propValue}" + ",";                                
                                else
                                    result += "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                            }
                            else
                            {
                                if (propValueType.Name == "Int32" || propValueType.Name == "Boolean")
                                    result += "\"" +$"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                else
                                    result += "\"" +$"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                                if (lastElement == prop)
                                {
                                    result += "}";
                                    int finalResultLength = result.Count();
                                    result = result.Remove(finalResultLength - 2, 1);
                                }
                            }
                        }                        
                    }                  
                }
            }
            return result;
        }
    }
}
