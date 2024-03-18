namespace Zhy.Components.Configuration.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? activeUser = AppConfig.Instance.ActiveUser;
            string? activeUserPassword = AppConfig.Instance.ActiveUserPassword;
            int? timeOut = AppConfig.Instance.TimeOut;
            Console.WriteLine($"activeUser: {activeUser}");
            Console.WriteLine($"activeUserPassword: {activeUserPassword}");
            Console.WriteLine($"timeOut: {timeOut}");
        }
    }
}
