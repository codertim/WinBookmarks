﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<LinkUrl> links = new List<LinkUrl>();

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
                LinkUrl linkUrl = new LinkUrl("", el.Value, false);
                links.Add(linkUrl);
            }
            AddAllUrlsToUi();
        }

        private void AddAllUrlsToUi()
        {
            foreach (var linkUrl in links)
            {
                AddUrlToUi(linkUrl.Url, linkUrl.isNew);
            }
        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {
            string url = (string) ((Button)sender).Content;
            System.Diagnostics.Process.Start(@"c:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe", url);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string url = (string) ((Button)sender).Tag;
            Trace.WriteLine("btnRemove_Click:", url);

            // remove link from internal list
            LinkUrl? linkToRemove = links.Find(x => x.Url == url);
            if (linkToRemove != null)
            {
                links.Remove(linkToRemove);
            }

            // refresh ui
            StackPanel1.Children.Clear();
            AddAllUrlsToUi();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUrlToListOfLinks(urlInput.Text);
            AddUrlToUi(urlInput.Text, true);
            ClearUrlField();
        }

        private void ClearUrlField()
        {
            urlInput.Text = "https://";
        }

        private void AddUrlToListOfLinks(string url)
        {
            LinkUrl newLinkUrl = new LinkUrl(String.Empty, url, true);
            links.Add(newLinkUrl);
        }

        private void AddUrlToUi(string url, bool isAllowingDelete = false)
        {
            DockPanel tempPanel = new DockPanel();
            ///tempPanel.Orientation = Orientation.Horizontal;

            int btnMargnThickness = 5;

            // link button
            Button tempBtn = new Button();
            tempBtn.Content = url;
            tempBtn.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x68, 0x82, 0x9E));
            tempBtn.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xAE, 0xBD, 0x38));
            tempBtn.HorizontalAlignment = HorizontalAlignment.Left;
            tempBtn.Margin = new Thickness(btnMargnThickness);
            tempBtn.Click += new RoutedEventHandler(btnBookmark_Click);
            tempPanel.Children.Add(tempBtn);

            if (isAllowingDelete)
            {
                // button for delete
                Button removeBtn = new Button();
                removeBtn.Content = "X";
                removeBtn.HorizontalAlignment = HorizontalAlignment.Right;
                removeBtn.Margin = new Thickness(btnMargnThickness);
                removeBtn.Padding = new Thickness(5);
                removeBtn.Click += new RoutedEventHandler(btnRemove_Click);
                removeBtn.Tag = url;
                tempPanel.Children.Add(removeBtn);
            }


            StackPanel1.Children.Add(tempPanel);
        }
    }
}
