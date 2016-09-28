using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Organizer.classs
{
    class ExtentionMethods
    {
        public static List<string> GetProperties(Type type)
        {
            List<string> returned = new List<string>();

            //Type atEntityTypeType = Type.GetType("Autotask_Webservice.net.autotask.webservices4." + cmbEntity.SelectedItem.ToString());
            PropertyInfo[] properties = type.GetProperties().OrderBy(o => o.Name).ToArray();
            foreach (PropertyInfo property in properties)
            {
                returned.Add(property.Name.ToString());
            }

            return returned;
        }

        public static object GetPropertyName(string propertyname, object source)
        {
            return source.GetType().GetProperty(propertyname).GetValue(source, null);
        }

        public static object SetPropertyValue(string propertyname, object value, object source)
        {
            object sourceobject = source;
            sourceobject.GetType().GetProperty(propertyname).SetValue(source, value);
            return sourceobject;
        }
    }
}
