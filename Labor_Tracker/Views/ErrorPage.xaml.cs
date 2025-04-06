using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Labor_Tracker.Views
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage(string errorMessage)
        {
            BindingContext = new ErrorViewModel(errorMessage);
        }
    }

    public class ErrorViewModel : BindableObject
    {
        public string ErrorMessage { get; }
        public ICommand RetryCommand { get; }

        public ErrorViewModel(string errorMessage)
        {
            ErrorMessage = errorMessage;
            RetryCommand = new Command(() => Retry());
        }

        private void Retry()
        {
            // Retry the login status check
            Application.Current.MainPage = new NavigationPage(new LoadingPage());
        }
    }
}
