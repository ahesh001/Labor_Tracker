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
        private readonly AuthenticationService _authService;

        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _message;

        public RegisterViewModel()
        {
            _authService = new AuthenticationService();
            RegisterCommand = new Command(async () => await RegisterAsync());
            NavigateToLoginCommand = new Command(async () => await NavigateToLoginAsync());
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
            //Input validation
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Please enter a username and password.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return;
            }

            var (isSuccess, msg) = await _authService.RegisterAsync(Username, Password);
            Message = msg;
            _message = isSuccess ? "Registration successful" : "User already exists";

            if (isSuccess)
            {
                // Navigate back to the login page after successful registration
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async Task NavigateToLoginAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}