using System.Data.Common;
using System.Reflection;
using System.Text;

namespace ModelInputGeneratorTemplateT4
{
    internal class Program
    {
        /* https://github.com/datablist/sample-csv-files?tab=readme-ov-file */

        private static string GetTypeForString (string str)
        {
            string type = string.Empty;

            if (Boolean.TryParse(str, out bool ignoreBool))
                type = "bool";
            else if (int.TryParse(str, out int ignoreInt))
                type = "int";
            else if (float.TryParse(str, out float ignoreFloat))
                type = "float";            
            else if (DateTime.TryParse(str, out DateTime ignoreDateTime))
                type = "DateTime";
            else
                type = "string";

            return type;
        }

        static void Main(string[] args)
        {
            string csvFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)??string.Empty, "Data", "customers-100.csv");
            char charSeparator = ',';
            string header = string.Concat("using Microsoft.ML.Data;", System.Environment.NewLine, System.Environment.NewLine, "namespace {0}", System.Environment.NewLine, "{{", System.Environment.NewLine, "\tpublic class {1}", System.Environment.NewLine, "\t{{", System.Environment.NewLine);
            string footer = string.Concat("\t}", System.Environment.NewLine, "}");
            string property = string.Concat("\t\t[ColumnName(\"{0}\"),LoadColumn({1})]", System.Environment.NewLine, "\t\tpublic {2} {3} {{ get; set; }}", System.Environment.NewLine);

            string[] columns = File.ReadLines(csvFile).Take(1).First().Split(new char[] { charSeparator });
            string[] firstLine = File.ReadLines(csvFile).Skip(1).Take(1).First().Split(new char[] { charSeparator });

            Console.WriteLine(string.Format(header, "InputModelNameSpace", "InputModel"));
             
            for (int i = 0; i < columns.Length; i++)
            {
                Console.WriteLine(string.Format(property, columns[i], i, GetTypeForString(firstLine[i]), columns[i].Substring(0, 1).ToUpper() + columns[i].Substring(1).Replace(" ", string.Empty)));
            }

            Console.WriteLine(footer);
        }
    }
}
