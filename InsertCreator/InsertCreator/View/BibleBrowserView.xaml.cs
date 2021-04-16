using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HgSoftware.InsertCreator.View
{
    /// <summary>
    /// Interaction logic for BibleBrowserView.xaml
    /// </summary>
    public partial class BibleBrowserView : UserControl
    {
        public BibleBrowserView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UrlProperty =
           DependencyProperty.RegisterAttached("Url", typeof(string), typeof(BibleBrowserView),
               new PropertyMetadata(OnUrlChanged));

        public static string GetUrl(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(UrlProperty);
        }

        public static void SetUrl(DependencyObject dependencyObject, string body)
        {
            dependencyObject.SetValue(UrlProperty, body);
        }

        private static void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var browser = d as WebBrowser;
            if (browser == null) return;

            Uri uri = null;

            var s = e.NewValue as string;

            if (s != null)
            {
                var uriString = s;

                uri = string.IsNullOrWhiteSpace(uriString) ? null : new Uri(uriString);
            }
            else if (e.NewValue is Uri)
            {
                uri = (Uri)e.NewValue;
            }

            browser.Source = uri;
        }
    }
}