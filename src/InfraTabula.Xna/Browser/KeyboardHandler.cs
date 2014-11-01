using System;
using System.Windows.Forms;
using CefSharp;

namespace InfraTabula.Xna
{
    public class KeyboardHandler : IKeyboardHandler
    {
        public bool OnKeyEvent(IWebBrowser browser, KeyType type, int code, int modifiers, bool isSystemKey)
        {
            var res = false;
            return res;
        }

        public bool OnPreKeyEvent(IWebBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, int modifiers,
            bool isSystemKey, bool isKeyboardShortcut)
        {
            var res = false;    // true means ignore input
            //if ((int) Keys.Up == windowsKeyCode || 
            //    (int) Keys.Down == windowsKeyCode)
            //{
            //    res = true;
            //}
            return res;
        }
    }
}
