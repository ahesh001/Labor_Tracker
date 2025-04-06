using Labor_Tracker.Views;

internal static class MainViewModelHelpers
{

    private static void OnLogout()
    {
        // Clear user session data
        SecureStorage.Default.Remove("user_id");

        MainThread.BeginInvokeOnMainThread(() =>
        {
            // Navigate back to the login page
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        });
    }
}