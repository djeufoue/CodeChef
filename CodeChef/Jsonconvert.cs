using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace CodeChef
{
    public static class Jsonconvert
    {

        static string result = null;
        static string canBeTheFirstValue = null;
        static string FirstElementMainList = null;
        static string LastElementMainList = null;
        /*public Type CheckObjectType(object? value)
        {
            Type valueType = value.GetType();
            if (valueType.Name == "Int32")
            {
                // Call a specific method for this one, 
            }
        }*/

        public static string SerializeThisObject(object? value)
        {
            Type valueType = value.GetType();
            List<object> obj = new List<object>();
            List<object> list = new List<object>();

            if (valueType.Name == "Int32")
                result = Convert.ToString(value);
            else if (valueType.Name == "Single")
                result = Convert.ToString(value);
            else if (valueType.Name == "Boolean")
                result = Convert.ToString(value);
            else if (valueType.Name == "String")
                result = Convert.ToString(value);
            //Serialization of Lists
            else if (value != null && valueType.Name == "List`1")
            {
                Type finalItemType = null;
                IEnumerable MainList = ((IEnumerable)value).Cast<object>().ToList();
                int count = 0;

                foreach (object item in MainList)
                {
                    count++;
                    Type itemType = item.GetType();

                    //Serialization of List<int>, List<float>, List<bool>
                    if (itemType.Name == "Int32" || itemType.Name == "Single" || itemType.Name == "Boolean")
                    {
                        finalItemType = itemType;
                        if (count == 1)
                            result = "[" + $"{item}" + ",";
                        else
                            result += $"{item}" + ",";
                    }
                    //Serialization of List<string>
                    else if (itemType.Name == "String")
                    {
                        finalItemType = itemType;
                        if (count == 1)
                            result = "[" + "\"" + $"{item}" + "\"" + ",";
                        else
                            result += "\"" + $"{item}" + "\"" + ",";

                    }
                    else
                    {
                        string[] propertyNames = item.GetType().GetProperties().Select(p => p.Name).ToArray();
                        string firstElement = propertyNames.First();
                        string lastElement = propertyNames.Last();
                        int propertyNameslength = propertyNames.Length;

                        //Serialization of List<Object> Eg: List<student>
                        foreach (var prop in propertyNames)
                        {
                            object propValue = item.GetType().GetProperty(prop).GetValue(item, null);

                            // to be remove Just in case the user didn't assign a value to a property. That property is not going to be print on the Json result                                               
                            if (propValue == null && lastElement == prop)
                            {
                                result += "},";
                                int finalResultLength = result.Count();
                                result = result.Remove(finalResultLength - 3, 1);
                            }
                            else if (propValue != null)
                            {
                                Type propValueType = propValue.GetType();

                                if (propertyNameslength == 1 && MainList.Cast<object>().Count() == 1)
                                {
                                    if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                        propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                        result = "[" + "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + "}" + "]";
                                    else
                                        result = "[" + "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + "}" + "]";
                                }
                                else
                                {
                                    if (firstElement == prop && count == 1)
                                    {
                                        if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                            propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                            result = "[" + "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                        else
                                            result = "[" + "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                                    }
                                    else if (count != 1 && firstElement == prop)
                                    {
                                        if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                            propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                            result += "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                        else
                                            result += "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                                    }
                                    else
                                    {
                                        if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                        propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                            result += "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                        else
                                            result += "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                                        if (lastElement == prop)
                                        {
                                            result += "},";
                                            int finalResultLength = result.Count();
                                            result = result.Remove(finalResultLength - 3, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (finalItemType != null)
                {
                    if (finalItemType.Name == "Int32" || finalItemType.Name == "Single" || finalItemType.Name == "Boolean" || finalItemType.Name == "String")
                    {
                        int finaltLength = result.Count();
                        result = result.Remove(finaltLength - 1, 1);
                        result += "]";
                    }
                }
                else
                {
                    int finaltLength = result.Count();
                    result = result.Remove(finaltLength - 1, 1);
                    result += "]";
                }
            }
            //Serialization of simple Object Eg: student, can handle properties of type integer, string, float, double, decimal and boolean
            else if (value != null && valueType.Name != "List`1")
            {
                obj.Add(value);

                foreach (object c in obj)
                {
                    string[] propertyNames = c.GetType().GetProperties().Select(p => p.Name).ToArray();
                    string firstElement = propertyNames.First();
                    string lastElement = propertyNames.Last();
                    int propertyNameslength = propertyNames.Length;


                    foreach (var prop in propertyNames)
                    {
                        object propValue = c.GetType().GetProperty(prop).GetValue(c, null);

                        if (propValue == null)
                        {
                            if (firstElement == prop)
                            {
                                result = "{" + "\"" + $"{prop}" + "\"" + ":" + "null,";
                                continue;
                            }
                            else if (lastElement == prop && firstElement != prop)
                            {
                                result += "\"" + $"{prop}" + "\"" + ":" + "null" + "}";
                                continue;
                            }
                            else if (lastElement == prop && firstElement == prop)
                            {
                                result = "{" + "\"" + $"{prop}" + "\"" + ":" + "null" + "}";
                                continue;
                            }
                            else
                            {
                                result += "\"" + $"{prop}" + "\"" + ":" + "null,";
                                continue;
                            }
                        }
                        else if (propValue != null)
                        {
                            Type propType = propValue.GetType();

                            if (propType.Name != "Int32" && propType.Name != "Boolean" && propType.Name != "Double" && propType.Name != "Single" &&
                                propType.Name != "Int64" && propType.Name != "Decimal" && propType.Name != "String")
                            {
                                string takeThisPropvalue = propValue.GetType().Name;

                                //Insert the comma after the "object" in this case
                                if (takeThisPropvalue == firstElement || takeThisPropvalue != lastElement)
                                {
                                    //Suppose to be the first value of the main list or not
                                    canBeTheFirstValue = takeThisPropvalue;
                                    FirstElementMainList = firstElement;

                                    result = SerializationObjectNode(propValue, canBeTheFirstValue);
                                    continue;
                                }
                                else if (takeThisPropvalue == lastElement)
                                {
                                    canBeTheFirstValue = takeThisPropvalue;
                                    LastElementMainList = lastElement;
                                    result = SerializationObjectNode(propValue, canBeTheFirstValue);
                                    result += "}";
                                    continue;
                                }
                            }

                            Type propValueType = propValue.GetType();

                            if (propertyNameslength == 1)
                            {
                                if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                    propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                    result = "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + "}";
                                else
                                    result = "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + "}";
                            }
                            else
                            {
                                if (firstElement == prop)
                                {
                                    if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                    propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                        result += "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                    else
                                        result += "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                                }
                                else
                                {
                                    if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                    propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                        result += "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                    else
                                        result += "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
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
            }
            return result;
        }

        static string firstProperty = null;
        static string lastProperty = null;

        //For recurcive function
        static string firstPropertyRecur = null;
        static string lastPropertyRecur = null;

        static string takeThisPropvalueRecur = null;

        static string ChecThisValueRecur = null;
        public static string SerializationObjectNode(object? value, string ChecThisValue)
        {
            Type objectType = value.GetType();

            if (firstPropertyRecur != null && lastPropertyRecur != null)
            {
                ChecThisValueRecur = ChecThisValue;
                if (ChecThisValueRecur == firstPropertyRecur)
                    result += "{" + "\"" + $"{objectType.Name}" + "\"" + ":";
                else
                    result += "\"" + $"{objectType.Name}" + "\"" + ":";
            }
            else if (FirstElementMainList != null || LastElementMainList != null)
            {
                if (ChecThisValue == FirstElementMainList)
                    result += "{" + "\"" + $"{objectType.Name}" + "\"" + ":";
                else
                    result += "\"" + $"{objectType.Name}" + "\"" + ":";

            }
            List<object> obj = new List<object>();
            obj.Add(value);

            foreach (object c in obj)
            {
                string[] propertyNames = c.GetType().GetProperties().Select(p => p.Name).ToArray();
                string firstElement = propertyNames.First();
                string lastElement = propertyNames.Last();
                int propertyNameslength = propertyNames.Length;
                Type propType = null;

                foreach (var prop in propertyNames)
                {
                    object propValue = c.GetType().GetProperty(prop).GetValue(c, null);

                    if (propValue == null)
                    {
                        firstProperty = firstElement;
                        lastProperty = lastElement;

                        //Call this method in case this property value is equal to null
                        result = ForNullProperty(prop);
                        continue;
                    }
                    else if (propValue != null)
                    {
                        propType = propValue.GetType();
                        if (propType.Name != "Int32" && propType.Name != "Boolean" && propType.Name != "Double" && propType.Name != "Single" &&
                                propType.Name != "Int64" && propType.Name != "Decimal" && propType.Name != "String")
                        {
                            takeThisPropvalueRecur = propValue.GetType().Name;
                            firstPropertyRecur = firstElement;
                            lastPropertyRecur = lastElement;
                            result = SerializationObjectNode(propValue, takeThisPropvalueRecur);

                            if(takeThisPropvalueRecur == firstElement && takeThisPropvalueRecur != lastElement)
                            {
                                result += ",";
                            }
                            else if (takeThisPropvalueRecur != firstElement && takeThisPropvalueRecur != lastElement)
                                result += ",";
                            else
                                result += "}";
                            continue;
                        }

                        Type propValueType = propValue.GetType();

                        if (propertyNameslength == 1)
                        {
                            if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                result = "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + "}";
                            else
                                result = "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + "}";
                        }
                        else
                        {
                            if (firstElement == prop)
                            {
                                if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                    result += "{" + "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                else
                                    result += "{" + "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
                            }
                            else
                            {
                                if (propValueType.Name == "Int32" || propValueType.Name == "Boolean" || propValueType.Name == "Double" ||
                                propValueType.Name == "Single" || propValueType.Name == "Int64" || propValueType.Name == "Decimal")
                                    result += "\"" + $"{prop}" + "\"" + ":" + $"{propValue}" + ",";
                                else
                                    result += "\"" + $"{prop}" + "\"" + ":" + "\"" + $"{propValue}" + "\"" + ",";
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
            if(ChecThisValueRecur != ChecThisValue)
            {
                if (ChecThisValue == FirstElementMainList || ChecThisValue != LastElementMainList)
                    result += ",";               
            }
            return result;
        }

        public static string ForNullProperty(object propName)
        {
            if (firstProperty == propName.ToString())
            {
                result = "{" + "\"" + $"{propName}" + "\"" + ":" + "null,";
            }
            else if (lastProperty == propName.ToString() && firstProperty != propName.ToString())
            {
                result += "\"" + $"{propName}" + "\"" + ":" + "null" + "}";
            }
            else if (lastProperty == propName.ToString() && firstProperty == propName.ToString())
            {
                result = "{" + "\"" + $"{propName}" + "\"" + ":" + "null" + "}";
            }
            else
            {
                result += "\"" + $"{propName}" + "\"" + ":" + "null,";
            }
            return result;
        }
    }
}
