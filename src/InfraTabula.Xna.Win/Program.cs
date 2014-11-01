using System;

namespace InfraTabula.Xna.Win
{
    static class Program
    {
        public static Game1 GameInstance;
        
        public static API API;



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CefConfig.Init();

            API = new PocketApi();

            GameInstance = new Game1();
            GameInstance.Services.AddService(typeof (API), API);

            GameInstance.Run();
        }



        public static void Debug(string message, params object[] args)
        {
            if (args != null && args.Length > 0)
                message = string.Format(message, args);

            System.Diagnostics.Debug.WriteLine(message);
        }

    }
}
