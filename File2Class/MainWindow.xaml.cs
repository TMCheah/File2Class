using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace File2Class
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Config config = ConfigManager.Instance.config;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void onLoaded(object sender, RoutedEventArgs e)
        {
            combo_outputLanguage.Text = "";
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            config.filePath = txt_filePath.Text;
            config.outputPath = txt_outputPath.Text;
            config.fileType = Path.GetExtension(txt_filePath.Text);
            config.language = combo_outputLanguage.Text;
            config.classNamespace = txt_namespace.Text;

            FileProcessing();
        }
        private void onlanguageChanged(object sender, EventArgs e)
        {
            config.language = combo_outputLanguage.Text;
        }
        private void onTextChanged_namespace(object sender, TextChangedEventArgs e)
        {
            config.classNamespace = txt_namespace.Text;
        }

        public void FileProcessing()
        {
            // TODO call ProcessFile and OutputFile;
            ProcessFile pf = new ProcessFile();
            pf.process();

            OutputFile of = new OutputFile(pf.classVariable);
            of.process();
        }

        
    }
}
