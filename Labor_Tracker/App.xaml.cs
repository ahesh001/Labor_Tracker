using Labor_Tracker.Views;

namespace Labor_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the default MainPage
            MainPage = new NavigationPage(new LoadingPage());

            // Perform the login status check
            CheckUserLoginStatus();
        }
        //Option to have the CheckUserLoginStatus method first when app starts
        protected override void OnStart()
        {
            base.OnStart();
            // or in the App constructor
            Task.Run(async () =>
            {
                var token = await SecureStorage.Default.GetAsync("firebase_token");
                if (!string.IsNullOrEmpty(token))
                {
                    // Possibly navigate to HomePage directly
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Shell.Current.GoToAsync("//HomePage");
                    });
                }
                else
                {
                    // Show login
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Shell.Current.GoToAsync("//LoginPage");
                    });
                }
            });
        }

        protected override void OnResume()
        {
            base.OnResume();

            //Checking to see if user is already logged in
            CheckUserLoginStatus();
        }


        private async void CheckUserLoginStatus()
        {
            try
            {
                // Logging for debugging:
                Console.WriteLine("CheckUserLoginStatus starting...");

                string userId = await SecureStorage.Default.GetAsync("user_id");
                Console.WriteLine($"UserID: {userId}");

                await Task.Delay(1000); 

                if (!string.IsNullOrEmpty(userId))
                {
                    // User is logged in, navigate to MainPage
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Console.WriteLine("Navigating to MainPage...");
                        MainPage = new NavigationPage(new HomePage());
                    });
                }
                else
                {
                    // User is not logged in, navigate to LoginPage
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Console.WriteLine("Navigating to LoginPage...");
                        MainPage = new NavigationPage(new LoginPage());
                    });
                }

                bool isLoggedIn = !string.IsNullOrEmpty(userId);
                if (isLoggedIn)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new NavigationPage(new HomePage());
                    });
                }
                else
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new NavigationPage(new LoginPage());
                    });
                }

            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., SecureStorage exceptions)
                Console.WriteLine($"Error checking login status: {ex.Message}");

                // Optionally display an error page or message to the user
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    MainPage = new NavigationPage(new ErrorPage(ex.Message));
                });
            }
        }
    }
}