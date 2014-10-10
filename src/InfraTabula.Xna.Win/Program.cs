using System;

namespace InfraTabula.Xna.Win
{
    static class Program
    {
        public static Game1 GameInstance;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameInstance = new Game1();
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
