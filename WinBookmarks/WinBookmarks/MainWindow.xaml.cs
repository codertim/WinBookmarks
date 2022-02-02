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
using System.Xml.Linq;

namespace WinBookmarks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // color scheme: Crisp and Dramatic
        private XDocument xDoc;

        public MainWindow()
        {
            InitializeComponent();

            // get urls from xml file
            IEnumerable<XElement> elements =  LoadXml();
            LoadBookmarkComponents(elements);
        }

        private IEnumerable<XElement> LoadXml()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            folderPath = folderPath + "\\MyWinData\\bookmarks.xml";
            xDoc = XDocument.Load(folderPath);
            //MessageBox.Show("xDoc:" + xDoc.ToString());
            IEnumerable<XElement> XElements = xDoc.Descendants("bookmark");
            return XElements;
        }

        private void LoadBookmarkComponents(IEnumerable<XElement> xElements)
        {
            foreach (XElement el in xElements)
            {
                Console.WriteLine("XElement value = ", el.Value);
                AddUrlToUi(el.Value);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {
            string url = (string) ((Button)sender).Content;
            System.Diagnostics.Process.Start(@"c:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", url);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUrlToUi(urlInput.Text);
            ClearUrlField();
        }

        private void ClearUrlField()
        {
            urlInput.Text = "https://";
        }

        private void AddUrlToUi(string url)
        {
            Button tempBtn = new Button();
            tempBtn.Content = url;
            tempBtn.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x68, 0x82, 0x9E));
            tempBtn.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xAE, 0xBD, 0x38));
            tempBtn.Click += new RoutedEventHandler(btnBookmark_Click);
            StackPanel1.Children.Add(tempBtn);
        }
    }
}
