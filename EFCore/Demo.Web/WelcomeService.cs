namespace Demo.Web
{
    public class WelcomeService : IWelcomeService
    {
        public string GetMessage()
        {
            return "hello from Iwelcome";
        }
    }
}
