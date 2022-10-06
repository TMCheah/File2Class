using System.Collections.Generic;
using System.Text;
using System.IO;

namespace File2Class
{
    class OutputFile
    {
        Dictionary<string, List<string>> classVariable;

        private Config config = ConfigManager.Instance.config;

        public OutputFile(Dictionary<string, List<string>> cv)
        {
            classVariable = cv;
        }

        public void process()
        {
            bool processSuccess = false;

            if (config.language.Equals("CSharp"))
                processSuccess = processCSFile();


            if (!processSuccess)
            {

            }
        }

        public bool processCSFile()
        {
            StringBuilder sb = new StringBuilder();
            int indentCount = 0;
            int openTagCount = 0;
            int closeTagCount = 0;

            foreach (KeyValuePair<string, List<string>> kvp in classVariable)
            {
                //using library
                if (config.fileType.Equals(".xml"))
                    sb.AppendLine("using System.Xml.Serialization;");

                sb.AppendLine();

                //check namespace?
                if (!config.classNamespace.Equals(""))
                {
                    sb.Append("namespace ").AppendLine(config.classNamespace);

                    //open namespace bracket
                    sb.AppendLine("{");

                    indentCount++;
                }

                for (int i = 0; i < indentCount; i++)
                {
                    sb.Append("\t");
                }

                //class name
                sb.Append("public class ").AppendLine(kvp.Key);

                for (int i = 0; i < indentCount; i++)
                {
                    sb.Append("\t");
                }

                sb.AppendLine("{");
                indentCount++;

                for (int j = 0; j < kvp.Value.Count; j++)
                {
                    for (int i = 0; i < indentCount; i++)
                    {
                        sb.Append("\t");
                    }

                    //check first letter if is uppercase or lowercase;
                    //uppercase is element, another class;
                    //lowercase is atribute.
                    if (kvp.Value[j][0] >= 65 && kvp.Value[j][0] <= 90)
                    {
                        //[ ] thingy
                        sb.Append("[XmlElement(\"").Append(kvp.Value[j]).AppendLine("\")]");
                        
                        for (int i = 0; i < indentCount; i++)
                        {
                            sb.Append("\t");
                        }

                        //the variable
                        sb.Append("public ").Append(kvp.Value[j]).Append(" ").Append(kvp.Value[j]).AppendLine(" { get; set; }");
                    }
                    else
                    {
                        //[ ] thingy
                        sb.Append("[XmlAttribute(\"").Append(kvp.Value[j]).AppendLine("\")]");

                        for (int i = 0; i < indentCount; i++)
                        {
                            sb.Append("\t");
                        }

                        //the variable
                        sb.Append("public string ").Append(kvp.Value[j]).AppendLine(" { get; set; }");
                    }
                }

                indentCount--;

                for (int i = 0; i < indentCount; i++)
                {
                    sb.Append("\t");
                }

                sb.AppendLine("}");

                //close namespace bracket
                if (!config.classNamespace.Equals(""))
                {
                    sb.AppendLine("}");
                }

                //end 1 class; output file;
                string fileName = kvp.Key + ".cs";

                if (File.Exists(Path.Combine(config.outputPath, fileName)))
                {
                    File.Delete(Path.Combine(config.outputPath, fileName));
                }

                File.AppendAllText(Path.Combine(config.outputPath, fileName), sb.ToString());

                sb.Clear();
                indentCount = 0;

            }

            

            return true;
        }




    }
}
