using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;

namespace Labor_Tracker.Services
{
    public class FirebaseAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FirebaseAuthService(HttpClient client, string apiKey)
        {
            _httpClient = client;
            _apiKey = apiKey;
        }

        public async Task<(bool IsSuccess, string Message)> RegisterAsync(string email, string password)
        {
            // call https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_apiKey}
            // handle response
        }

        public async Task<(bool IsSuccess, string Message)> LoginAsync(string email, string password)
        {
            // call https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}
            // handle response
        }
    }
}
