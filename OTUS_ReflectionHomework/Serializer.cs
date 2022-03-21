using System;
using System.Reflection;
using System.Text;

namespace OTUS_ReflectionHomework
{
    class Serializer
    {
        private static string _colSeparator = ",";
        private static string _rowSeparator = Environment.NewLine;
        public static string SerializeToCSV(object obj)
        {
            StringBuilder csvString = new StringBuilder();
            FieldInfo[] fields = obj.GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                if (!fields[i].IsNotSerialized)
                {
                    csvString.Append(fields[i].Name);
                    csvString.Append(_colSeparator);
                    csvString.Append(fields[i].GetValue(obj)?.ToString().Replace("\r", "").Replace(_rowSeparator, "").Replace(_colSeparator,""));
                    csvString.Append(_rowSeparator);
                }
            }
            return csvString.ToString();
        }
        public static T DeserializeFromCSV<T>(string data)
        {
            string[] csvRows = data.Split(_rowSeparator, StringSplitOptions.RemoveEmptyEntries);

            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields();
            object output = Activator.CreateInstance(type);

            for (int i = 0; i < csvRows.Length; i++)
            {
                string[] cells = csvRows[i].Split(_colSeparator, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < fields.Length; j++)
                {
                    if(fields[j].Name == cells[0])
                    {
                        fields[j].SetValue(output, Convert.ChangeType(cells[1], Type.GetTypeCode(fields[j].FieldType)));
                        break;
                    }
                }
            }

            return (T)output;
        }
    }
}
