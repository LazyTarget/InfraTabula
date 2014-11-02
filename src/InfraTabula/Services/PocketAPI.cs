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
                Tags = item.Tags,
            };
            return res;
        }


        private ActionResult Modify(PocketAction action)
        {
            var actions = new List<PocketAction> { action };
            var results = _api.Modify(actions).ToList();
            var res = results.Single();
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


        public override object AddTags(object id, string tags)
        {
            var action = new TagsAddPocketAction
            {
                ItemID = System.Convert.ToInt32(id),
                Tags = tags,
            };
            var actionResult = Modify(action);
            var result = actionResult.Result;
            return result;
        }


        public override object RemoveTags(object id, string tags)
        {
            var action = new TagsRemovePocketAction
            {
                ItemID = System.Convert.ToInt32(id),
                Tags = tags,
            };
            var actionResult = Modify(action);
            var result = actionResult.Result;
            return result;
        }


    }
}
