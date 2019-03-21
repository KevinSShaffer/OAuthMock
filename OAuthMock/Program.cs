using System;

namespace OAuthMock
{
    class Program
    {
        static void Main(string[] args)
        {
            var authorizationServer = new AuthorizationServer();
            var api = new Api(authorizationServer);
            var application = new Application(authorizationServer, api);

            application.Start();

            Console.ReadKey();
        }
    }
}
