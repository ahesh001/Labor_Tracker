using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Labor_Tracker.Services;
using Microsoft.Maui.Controls;

namespace Labor_Tracker.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        private readonly FirebaseAuthService _authService;

        private string _email;
        private string _password;
        private string _confirmPassword;
        private string _message;

        public RegisterViewModel()
        {
            // Normally you'd store the API key in a config or secure place
            _authService = new FirebaseAuthService(new HttpClient(), "YOUR_FIREBASE_API_KEY");

            RegisterCommand = new Command(async () => await RegisterAsync());
            NavigateToLoginCommand = new Command(async () => await NavigateToLoginAsync());
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter an email and password.";
                return;
            }
            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return;
            }

            var (isSuccess, msg, idToken) = await _authService.RegisterAsync(Email, Password);
            Message = msg;

            if (isSuccess && !string.IsNullOrEmpty(idToken))
            {
                // Store token in SecureStorage if you like
                await SecureStorage.Default.SetAsync("firebase_token", idToken);

                // Navigate back to login
                await Shell.Current.GoToAsync("//LoginPage");
                // or however you're handling navigation
            }
        }

        private async Task NavigateToLoginAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}