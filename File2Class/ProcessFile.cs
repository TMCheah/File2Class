using System;
using System.IO;
using System.Collections.Generic;

namespace File2Class
{
    class ProcessFile
    {
        public Dictionary<string, List<string>> classVariable { get; set; }

        private Config config = ConfigManager.Instance.config;

        public ProcessFile()
        {
            classVariable = new Dictionary<string, List<string>>();
        }

        public void process()
        {
            bool processSuccess = false;

            if (config.fileType.Equals(".xml"))
                processSuccess = processXML();


            if (!processSuccess)
            { 
                
            }

        }

        public bool processXML()
        {
            if (!File.Exists(config.filePath))
            {
                return false;
            }

            string[] readFile = File.ReadAllLines(config.filePath);

            bool isXMLElementNext = false;
            string className = "";

            foreach (string line in readFile)
            {
                string[] splitLine = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                string rootClassName = "";

                foreach (string sl in splitLine)
                {

                    string variable = "";

                    if (sl.StartsWith("<?") || sl.EndsWith("?>"))
                        continue;

                    //closing of xml tag   //sl.StartsWith("</") || 
                    if (sl.Contains("</"))
                    {
                        // TODO handle </

                        if (isXMLElementNext)
                            isXMLElementNext = false;

                        continue;
                    }

                    //beginning of the xml tag
                    //skip the version and namespace
                    if (!sl.StartsWith("<?") && !sl.StartsWith("</") && sl.StartsWith("<"))
                    {
                        //root here denote the parent of following element
                        if (isXMLElementNext)
                            rootClassName = className;
                        else
                            className = "";
                        
                        //remove the leading '<'
                        className = sl.Trim().Remove(0, 1);

                        //remove the trailing '>'
                        if (sl.Contains(">"))
                            className = className.Remove(className.IndexOf('>'), className.Length- className.IndexOf('>'));

                        //remove namesapce
                        if (className.Contains(":"))
                            className = className.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                        //make the first char of the class name uppercase
                        className = char.ToUpper(className[0]) + className.Substring(1);

                        if(!classVariable.ContainsKey(className))
                            classVariable.Add(className, new List<string>());

                        //insert the current class as element of root class
                        if (isXMLElementNext)
                        {
                            classVariable[rootClassName].Add(className);
                            rootClassName = "";
                        }
                    }

                    //attribute of XML
                    if (sl.Contains("="))
                    {
                        variable = sl.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                        if(!classVariable[className].Contains(variable))
                            classVariable[className].Add(variable);
                    }

                    if (!sl.EndsWith("/>") && sl.EndsWith(">") && !sl.StartsWith("<"))
                    {
                        // TODO handle XML Element for next line;
                        isXMLElementNext = true;
                    }

                    if (sl.Equals("/>") || sl.EndsWith("/>"))
                    {
                        // TODO handle />

                        if (isXMLElementNext)
                            isXMLElementNext = false;

                        continue;
                    }

                }
            }

            return true;
        }


    }
}
