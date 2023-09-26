using System.Diagnostics;

namespace CampingApp.Views;

public partial class CurrentTempView
{
	public CurrentTempView()
	{
		InitializeComponent();
	}

	async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
		try {
			await Shell.Current.GoToAsync($"//radar");
		} catch (Exception ex) {
			Debug.WriteLine($"err: {ex.Message}");
		}
	}
}