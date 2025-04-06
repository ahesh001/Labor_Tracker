using System.Windows.Input;
using Labor_Tracker.Models;
using Labor_Tracker.Views;
using Microsoft.Maui.Controls;

namespace Labor_Tracker.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        public ICommand LogoutCommand { get; }

        // ----- Current Trip Properties -----
        private string _currentTruckId;
        private string _currentDriver;
        private string _currentLoad;
        private DateTime _currentStartTime;
        private DateTime _currentEndTime;
        private TimeSpan _currentDuration;

        // ----- Previous Trip Properties -----
        private string _previousTruckId;
        private string _previousDriver;
        private string _previousLoad;
        private DateTime _previousStartTime;
        private DateTime _previousEndTime;
        private TimeSpan _previousDuration;

        public HomeViewModel()
        {

                CurrentTruckId = "T123";
                CurrentDriver = "John Doe";
                CurrentLoad = "Building Materials";
                CurrentStartTime = DateTime.Now.AddHours(-1);  // 1 hour ago
                CurrentEndTime = DateTime.Now;
                CurrentDuration = CurrentEndTime - CurrentStartTime;

                PreviousTruckId = "T456";
                PreviousDriver = "Jane Smith";
                PreviousLoad = "Electrical Goods";
                PreviousStartTime = DateTime.Now.AddHours(-3); // 3 hours ago
                PreviousEndTime = DateTime.Now.AddHours(-2);   // 2 hours ago
                PreviousDuration = PreviousEndTime - PreviousStartTime;
            
            LogoutCommand = new Command(OnLogout);
            Console.WriteLine("Logout button pressed");
        }

        // Current Trip
        public string CurrentTruckId
        {
            get => _currentTruckId;
            set { _currentTruckId = value; OnPropertyChanged(); }
        }

        public string CurrentDriver
        {
            get => _currentDriver;
            set { _currentDriver = value; OnPropertyChanged(); }
        }

        public string CurrentLoad
        {
            get => _currentLoad;
            set { _currentLoad = value; OnPropertyChanged(); }
        }

        public DateTime CurrentStartTime
        {
            get => _currentStartTime;
            set { _currentStartTime = value; OnPropertyChanged(); }
        }

        public DateTime CurrentEndTime
        {
            get => _currentEndTime;
            set { _currentEndTime = value; OnPropertyChanged(); }
        }

        public TimeSpan CurrentDuration
        {
            get => _currentDuration;
            set { _currentDuration = value; OnPropertyChanged(); }
        }

        // Previous Trip
        public string PreviousTruckId
        {
            get => _previousTruckId;
            set { _previousTruckId = value; OnPropertyChanged(); }
        }

        public string PreviousDriver
        {
            get => _previousDriver;
            set { _previousDriver = value; OnPropertyChanged(); }
        }

        public string PreviousLoad
        {
            get => _previousLoad;
            set { _previousLoad = value; OnPropertyChanged(); }
        }

        public DateTime PreviousStartTime
        {
            get => _previousStartTime;
            set { _previousStartTime = value; OnPropertyChanged(); }
        }

        public DateTime PreviousEndTime
        {
            get => _previousEndTime;
            set { _previousEndTime = value; OnPropertyChanged(); }
        }

        public TimeSpan PreviousDuration
        {
            get => _previousDuration;
            set { _previousDuration = value; OnPropertyChanged(); }
        }

        private void OnLogout()
        {
            try
            {
                SecureStorage.Default.Remove("user_id");

                // Navigate back to the LoginPage
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
