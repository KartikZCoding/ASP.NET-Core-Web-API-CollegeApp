namespace CollegeApp.MyLogging
{
    public class LogToMemoryServer : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToMemoryServer");
        }
    }
}
