using System;
using System.Collections.Generic;

namespace OAuthMock
{
    class Application
    {
        private Dictionary<string, string> credentials;
        private Dictionary<string, string> userTokens;
        private readonly AuthorizationServer authorizationServer;
        private readonly Api api;

        public Application(AuthorizationServer authorizationServer, Api api)
        {
            credentials = new Dictionary<string, string>();
            userTokens = new Dictionary<string, string>();
            this.authorizationServer = authorizationServer;
            this.api = api;
        }

        public void Start()
        {
            string username = Register();

            Console.Clear();
            Console.WriteLine("Hello!  Let's now use Oauth to authenticate!");
            Console.ReadKey();

            string grant = authorizationServer.Authenticate();

            UserAuthenticated(grant, username);

            Console.WriteLine($"Your age is {api.GetAge(username, userTokens[username])}");
        }

        public string Register()
        {
            Console.WriteLine("Hello!  Please register.");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            credentials.Add(username, password);

            return username;
        }

        public void UserAuthenticated(string grant, string username)
        {
            string token = authorizationServer.RequestToken(grant, "digital signature?");

            if (!string.IsNullOrEmpty(token))
                userTokens.Add(username, token);
        }
    }
}
