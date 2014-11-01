using System;
using System.Linq;
using CefSharp;

namespace InfraTabula.Xna
{
    public static class CefConfig
    {
        //public const string DefaultUrl = "custom://cefsharp/home";
        public const string DefaultUrl = "http://www.google.com";

        // Use when debugging the actual SubProcess, to make breakpoints etc. inside that project work.
        private const bool debuggingSubProcess = false;

        public static void Init()
        {
            var settings = new CefSettings();
            settings.RemoteDebuggingPort = 8088;
            settings.LogSeverity = LogSeverity.Verbose;

            if (debuggingSubProcess)
            {
                settings.BrowserSubprocessPath = "..\\..\\..\\..\\CefSharp.BrowserSubprocess\\bin\\x86\\Debug\\CefSharp.BrowserSubprocess.exe";
            }

            //settings.RegisterScheme(new CefCustomScheme
            //{
            //    SchemeName = CefSharpSchemeHandlerFactory.SchemeName,
            //    SchemeHandlerFactory = new CefSharpSchemeHandlerFactory()
            //});


            if (!Cef.Initialize(settings))
            {
                if (Environment.GetCommandLineArgs().Contains("--type=renderer"))
                {
                    Environment.Exit(0);
                }
                else
                {
                    return;
                }
            }

            Cef.RegisterJsObject("bound", new BoundObject());
        }
    }
}
