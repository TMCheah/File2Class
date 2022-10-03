using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File2Class
{
    class OutputFile
    {
        string outputPath { get; set; }
        string outputLanguage { get; set; }
        string fileType { get; set; }

        Dictionary<string, List<string>> classVariable;

        public OutputFile(string path, string type, string language)
        {
            outputPath = path;
            fileType = type;
            outputLanguage = language;

        }

        public void process()
        {
            bool processSuccess = false;

            if (outputLanguage.Equals("C#"))
                processSuccess = processCSFile();


            if (!processSuccess)
            {

            }
        }

        public bool processCSFile()
        {




            return true;
        }




    }
}
