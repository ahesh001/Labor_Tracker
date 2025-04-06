using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Labor_Tracker.Views;
using Microsoft.Maui.Controls;

namespace Labor_Tracker.ViewModels
{
    public class MainViewModel : BindableObject 
    {
        public MainViewModel()
        {
        }

        /*private async Task LogoutAsync()
        {
            // Clear user session data
            SecureStorage.Default.Remove("user_id");

            // Navigate back to the login page
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }*/
    }
}
