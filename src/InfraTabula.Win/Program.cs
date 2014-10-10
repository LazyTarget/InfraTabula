using System;

namespace InfraTabula.Win
{
    static class Program
    {
        public static App App;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            App = new App();
            App.Run();
        }


        public static void Debug(string message, params object[] args)
        {
            if (args != null && args.Length > 0)
                message = string.Format(message, args);

            System.Diagnostics.Debug.WriteLine(message);
        }

    }
}
