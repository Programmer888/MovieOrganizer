using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Movie_Organizer.classs
{
    [Serializable]
    public static class Settings
    {
        private static Settings_Instance instance;

        public static string MoviePath { get; set; }

        public static string SeriePath { get; set; }

        public static List<string> Exceptions { get; set; }

        public static List<string> Extentions { get; set; }

        //public static List<string> Directorytree { get; set; }






        public static void Read()
        {
            instance = new Settings_Instance();

            if (File.Exists("settings.xml"))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(Settings_Instance));
                FileStream myFileStream = new FileStream("settings.xml", FileMode.Open);

                instance = (Settings_Instance)mySerializer.Deserialize(myFileStream);

                myFileStream.Close();

                // some code removed ...
                Type settingsType = typeof(Settings);   // Settings is a static class

                foreach (PropertyInfo propertyInformation in settingsType.GetProperties(BindingFlags.Public |
                                                  BindingFlags.Static))
                {

                    object value = ExtentionMethods.GetPropertyName(propertyInformation.Name.ToString(), instance);

                    propertyInformation.SetValue(null, value, null);
                    //propertyInformation.SetValue(this, Convert.ChangeType(value, propertyInformation.PropertyType, CultureInfo.CurrentCulture), null);
                }

            }
        }

        public static void Write()
        {
            foreach (PropertyInfo propertyInformation in typeof(Settings).GetProperties(BindingFlags.Public |
                                                  BindingFlags.Static))
            {
                // object value = ExtentionMethods.GetPropertyName(property, null);
                object value = propertyInformation.GetValue(null);
                instance = (Settings_Instance)ExtentionMethods.SetPropertyValue(propertyInformation.Name, value, instance);

            }
            //foreach(string property in ExtentionMethods.GetProperties(instance.GetType()))
            //{
            //    object value = ExtentionMethods.GetPropertyName(property, null);
            //    instance = (Settings_Instance)ExtentionMethods.SetPropertyValue(property, instance);
            //}

            XmlSerializer mySerializer = new XmlSerializer(typeof(Settings_Instance));
            StreamWriter myWriter = new StreamWriter("settings.xml");
            mySerializer.Serialize(myWriter, instance);
            myWriter.Close();

        }
    }

    public class Settings_Instance
    {
        public string MoviePath { get; set; }

        public string SeriePath { get; set; }

        public List<string> Exceptions { get; set; }

        public List<string> Extentions { get; set; }


    }
}
