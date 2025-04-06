using Labor_Tracker.ViewModels;

namespace Labor_Tracker.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new HomeViewModel();
    }

}