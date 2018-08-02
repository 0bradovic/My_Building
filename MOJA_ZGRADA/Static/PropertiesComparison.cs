using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Static
{
    public static class PropertiesComparison
    {
        //Template method: insert two classes, add properties from 2nd to 1st if they are not null in 2nd class (skip nulls)
        public static void CompareAndForward<TOne, TTwo>(TOne Class1, TTwo Class2) where TOne : class where TTwo : class
        {
            Type typeB = Class1.GetType();

            foreach (PropertyInfo property1 in Class1.GetType().GetProperties())
            {
                foreach (PropertyInfo property2 in Class2.GetType().GetProperties())
                {
                    var x = property2.GetValue(Class2);
                    if (property1.Name == property2.Name)
                    { 
                        if (!property2.CanRead || (property2.GetIndexParameters().Length > 0))
                            continue;

                        PropertyInfo other = typeB.GetProperty(property2.Name);
                        if ((other != null) && (other.CanWrite))
                        {
                            other.SetValue(Class1, property2.GetValue(Class2, null), null);
                            break;
                        }
                    }
                }
            }


        }
    }
}