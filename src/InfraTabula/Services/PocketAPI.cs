using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PocketAPI;

namespace InfraTabula
{
    public class PocketApi : API
    {
        private readonly PocketAPI.PocketApi _api;

        public PocketApi()
        {
            _api = new PocketAPI.PocketApi
            {
                ConsumerKey = "25447-07c62f25a2a1ca3066679052",
                RedirectUri = "http://www.xhaus.com/headers",
            };
            _api.OnConfirmRequired += Pocket_OnConfirmRequired;
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
            if (args.Url.AbsoluteUri == _api.RedirectUri)
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


        private Item Convert(PocketAPI.Item item)
        {
            var res = new Item
            {
                ID = item.ItemID.ToString(),
                Title = item.ResolvedTitle,
                Url = item.ResolvedUrl,
            };
            return res;
        }





        public override IEnumerable<Item> GetItems()
        {
            var enumerable = _api.GetItems();
            if (enumerable == null)
                return null;
            var res = enumerable.Select(Convert);
            return res;
        }
    }
}
