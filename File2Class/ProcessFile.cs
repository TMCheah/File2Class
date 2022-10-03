using System;
using System.IO;
using System.Collections.Generic;

namespace File2Class
{
    class ProcessFile
    {
        string filePath { get; set; }
        string fileType { get; set; }

        Dictionary<string, List<string>> classVariable;

        public ProcessFile(string path, string type)
        {
            filePath = path;
            fileType = type;
        }

        public void process()
        {
            bool processSuccess = false;

            if (fileType.Equals("xml"))
                processSuccess = processXML();


            if (!processSuccess)
            { 
                
            }

        }

        public bool processXML()
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            string[] readFile = File.ReadAllLines(filePath);

            bool isXMLElementNext = false;

            foreach (string line in readFile)
            {
                string[] splitLine = line.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                string className = "";
                string rootClassName = "";

                foreach (string sl in splitLine)
                {

                    string variable = "";

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
                        
                        //remove the leading '<'
                        className = sl.Trim().Remove(0, 1);

                        //remove the trailing '>'
                        if (sl.Contains(">"))
                            className = className.Remove(className.Length-1, 1);

                        //remove namesapce
                        if (className.Contains(":"))
                            className = className.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                        //make the first char of the class name uppercase
                        className = char.ToUpper(className[0]) + className.Substring(1);

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
                        classVariable[className].Add(variable);
                    }

                    if (!sl.EndsWith("/>") && sl.EndsWith(">"))
                    {
                        // TODO handle XML Element for next line;
                        isXMLElementNext = true;
                    }

                    if (sl.Equals("/>"))
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
