using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        // Data class for Firebase auth response
        private class FirebaseAuthResponse
        {
            public string idToken { get; set; }
            public string email { get; set; }
            public string refreshToken { get; set; }
            public string localId { get; set; }
            public string displayName { get; set; }
        }

        /// <summary>
        /// Registers a new user with email/password on Firebase.
        /// </summary>
        public async Task<(bool IsSuccess, string Message, string IdToken)> RegisterAsync(string email, string password)
        {
            // Endpoint for signing up new users

            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_apiKey}";

            var payload = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode) 
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                return (false, $"Register error: {errorBody}", null);
            }

            var body = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<FirebaseAuthResponse>(body);

            // If success, we have an idToken, localId, etc
            return (true, "Registration successful", authResponse.idToken);
        }

        /// <summary>
        /// Logs in an existing user with email/password on Firebase.
        /// </summary>
        public async Task<(bool IsSuccess, string Message, string IdToken)> LoginAsync(string email, string password)
        {
            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}";

            var payload = new
            {
                email,
                password,
                returnSecureToken = true
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                return (false, $"Login error: {errorBody}", null);
            }

            var body = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<FirebaseAuthResponse>(body);

            return (true, "Login successful", authResponse.idToken);
        }
    }
}
