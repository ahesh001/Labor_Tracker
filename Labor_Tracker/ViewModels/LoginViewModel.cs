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
        private readonly AuthenticationService _authService;

        private string _username;
        private string _password;
        private string _message;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
            _authService = new AuthenticationService();
            //LoginCommand = new Command(async () => await LoginAsync());
            NavigateToRegisterCommand = new Command(async () => await NavigateToRegisterAsync());
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
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

        private void OnLogin()
        {

            Console.WriteLine($"Attempting login with user={Username} pass={Password}");
            // Basic example - in real usage, you'd check credentials or call an API
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                // Navigate to HomePage
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = new NavigationPage(new HomePage());
                });
            }
            else
            {
                Message = "Invalid username or password.";
            }
        }

        public ICommand NavigateToRegisterCommand { get; }

        /*private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter your username and password.";
                return;
            }
            
            var (isSuccess, msg, user) = await _authService.LoginAsync(Username, Password);
            Message = msg;

            if (isSuccess)
            {
                // Store user session data securely
                await SecureStorage.Default.SetAsync("user_id", user.Id.ToString());

                // Navigate to the main page
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
        }*/

        private async Task NavigateToRegisterAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}