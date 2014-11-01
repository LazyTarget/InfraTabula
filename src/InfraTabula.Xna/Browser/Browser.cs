using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace InfraTabula.Xna
{
    public partial class Browser : Form
    {
        public ChromiumWebBrowser WebBrowser { get; private set; }
        
        public Browser(string url)
        {
            InitializeComponent();

            //WebBrowser = new ChromiumWebBrowser("chrome://version");
            WebBrowser = new ChromiumWebBrowser(url);
            WebBrowser.Dock = DockStyle.Fill;
            WebBrowser.MenuHandler = new MenuHandler();
            WebBrowser.AddressChanged += delegate(object sender, AddressChangedEventArgs args)
            {
                
            };
            WebBrowser.NavStateChanged += delegate(object sender, NavStateChangedEventArgs args)
            {
                
            };
            WebBrowser.TitleChanged += delegate(object sender, TitleChangedEventArgs args)
            {
                
            };
            WebBrowser.IsLoadingChanged+= delegate(object sender, IsLoadingChangedEventArgs args)
            {

            };
            WebBrowser.ClientSizeChanged += delegate(object sender, EventArgs args)
            {
                //url = "http://google.com";
                //WebBrowser.Load(url);
            };
            panel1.Controls.Add(WebBrowser);
        }


        public void Navigate(string url)
        {
            WebBrowser.Load(url);
        }

        private class MenuHandler : CefSharp.IMenuHandler
        {
            public bool OnBeforeContextMenu(IWebBrowser browser)
            {
                return true;
            }
        }
    }
}
