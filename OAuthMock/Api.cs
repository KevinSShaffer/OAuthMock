using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthMock
{
    class Api
    {
        private readonly Dictionary<string, int> ages;
        private readonly AuthorizationServer authorizationServer;

        public Api(AuthorizationServer authorizationServer)
        {
            ages = new Dictionary<string, int>()
            {
                { "kevin", 33 }
            };
            this.authorizationServer = authorizationServer;
        }

        public int GetAge(string username, string token)
        {
            if (authorizationServer.TokenValid(token) &&
                ages.ContainsKey(username))
                return ages[username];
            else
                return -1;
        }
    }
}
