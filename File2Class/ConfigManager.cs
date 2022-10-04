namespace File2Class
{
    //singleton 
    class ConfigManager
    {
        private static ConfigManager _instance = null;
        public Config config = new Config();


        public static ConfigManager Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new ConfigManager();
                }

                return _instance;
            }
        }

    }

    public class Config
    {
        public string filePath { get; set; }
        public string outputPath { get; set; }
        public string fileType { get; set; }
        public string language { get; set; }
        public string classNamespace { get; set;}
    }
}
