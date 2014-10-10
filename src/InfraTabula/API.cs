using System;
using System.Windows.Forms;
using PocketAPI;

namespace InfraTabula
{
    public class API : PocketApi
    {
        private static readonly Lazy<API> _instance = new Lazy<API>(() => new API());

        public static API Instance
        {
            get { return _instance.Value; }
        }


        private API()
        {
            ConsumerKey = "25447-07c62f25a2a1ca3066679052";
            RedirectUri = "http://www.xhaus.com/headers";
            OnConfirmRequired += Pocket_OnConfirmRequired;
        }

        private ConfirmEventArgs _confirmEventArgs;


        private void Pocket_OnConfirmRequired(object sender, ConfirmEventArgs args)
        {
            _confirmEventArgs = args;

            var b = new TestConsole.Browser(args.ConfirmUrl);
            b.Navigated += OnNavigated;

            Application.Run(b);
        }


        private void OnNavigated(object sender, WebBrowserNavigatedEventArgs args)
        {
            var browser = (TestConsole.Browser)sender;
            //var doc = browser.GetDocument();
            if (args.Url.AbsoluteUri == RedirectUri)
            {
                browser.Close();
                _confirmEventArgs.OnConfirm();
            }
            else
            {
                browser.Visible = true;
                browser.WindowState = FormWindowState.Normal;
            }
        }

    }
}
