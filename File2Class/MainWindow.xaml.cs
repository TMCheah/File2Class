using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace File2Class
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _filePath;
        string _outputPath;
        string _language;

        Dictionary<string, List<string>> classVariable;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            _filePath = txt_filePath.Text;
            _outputPath = txt_outputPath.Text;
            _language = combo_outputLanguage.SelectedItem.ToString();
            FileProcessing();
        }

        public void FileProcessing()
        { 
            // TODO determine the extension of the file



        }


    }
}
