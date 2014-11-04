namespace InfraTabula.Xna
{
    public static class LogExtensions
    {
        public static DebugLogger Log(this object obj)
        {
            var l = new DebugLogger();
            return l;
        }


        public class DebugLogger
        {
            public void Info(string message, params object[] args)
            {
                System.Diagnostics.Debug.WriteLine(message, args);
            }
        }
    }
}
