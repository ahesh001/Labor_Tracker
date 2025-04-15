using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Labor_Tracker.Services;
using Microsoft.Maui.Controls;
using Labor_Tracker.Views;

namespace Labor_Tracker.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private readonly FirebaseAuthService _authService;

        private string _email;
        private string _password;
        private string _message;

        public LoginViewModel()
        {
            _authService = new FirebaseAuthService(new HttpClient(), "YOUR_FIREBASE_API_KEY");
            LoginCommand = new Command(async () => await LoginAsync());
            NavigateToRegisterCommand = new Command(async () => await NavigateToRegisterAsync());
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

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Enter email and password.";
                return;
            }

            (bool isSuccess, string msg, string idToken) = await _authService.LoginAsync(Email, Password);
            Message = msg;

            if (isSuccess && !string.IsNullOrEmpty(idToken))
            {
                // Store the token in SecureStorage
                await SecureStorage.Default.SetAsync("firebase_token", idToken);

                // Navigate to your home page
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
        }

        private async Task NavigateToRegisterAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}