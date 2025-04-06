using System.Windows.Input;
using Labor_Tracker.Views;
using Microsoft.Maui.Controls;

namespace Labor_Tracker.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        public ICommand LogoutCommand { get; }
        

        public HomeViewModel()
        {
            LogoutCommand = new Command(OnLogout);
        }

        private void OnLogout()
        {
            try
            {
                SecureStorage.Default.Remove("user_id");
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout error: {ex}");
            }
        }

    }
}
