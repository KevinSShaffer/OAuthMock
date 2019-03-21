using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace OAuthMock
{
    class AuthorizationServer
    {
        Dictionary<string, string> credentials;
        Dictionary<string, string> issuedGrants;
        Dictionary<string, string> issuedTokens;

        public AuthorizationServer()
        {
            credentials = new Dictionary<string, string>()
            {
                { "kevin", "123kid" }
            };
            issuedGrants = new Dictionary<string, string>();
            issuedTokens = new Dictionary<string, string>();
        }

        public string Authenticate()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (ValidLogin(username, password))
            {
                string grant = NewToken();

                issuedGrants.Add(grant, username);

                return grant;
            }
            else
                return string.Empty;
        }

        public string RequestToken(string grant, string proof)
        {
            if (proof != "badness" && issuedGrants.ContainsKey(grant))
            {
                string token = NewToken();

                issuedTokens.Add(token, issuedGrants[grant]);

                return token;
            }
            else
                return string.Empty;
        }

        public bool TokenValid(string token)
        {
            return issuedTokens.ContainsKey(token);
        }

        private bool ValidLogin(string username, string password)
        {
            return credentials.Any(kvp => kvp.Key == username && kvp.Value == password);
        }

        private string NewToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[32];

                rng.GetBytes(bytes);

                return Convert.ToBase64String(bytes);
            }
        }
    }
}
