using System.Diagnostics;

namespace CampingApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
		FlashlightSwitch.IsToggled = !FlashlightSwitch.IsToggled;
	}

	private async void FlashlightSwitch_Toggled(object sender, ToggledEventArgs e) {
		try {
			if (FlashlightSwitch.IsToggled)
				await Flashlight.Default.TurnOnAsync();
			else
				await Flashlight.Default.TurnOffAsync();
		} catch (FeatureNotSupportedException ex) {
			// Handle not supported on device exception
		} catch (PermissionException ex) {
			// Handle permission exception
		} catch (Exception ex) {
			// Unable to turn on/off flashlight
		}
	}

	private async void Button_Clicked(object sender, EventArgs e) {
		try {
			await Shell.Current.GoToAsync($"//pdf");
		} catch (Exception ex) {
			Debug.WriteLine($"err: {ex.Message}");
		}

	}

	//private void BatterySwitch_Toggled(object sender, ToggledEventArgs e) =>
	//WatchBattery();

	//private bool _isBatteryWatched = false;

	//private void WatchBattery() {

	//	if (!_isBatteryWatched) {
	//		Battery.Default.BatteryInfoChanged += Battery_BatteryInfoChanged;
	//	} else {
	//		Battery.Default.BatteryInfoChanged -= Battery_BatteryInfoChanged;
	//	}

	//	_isBatteryWatched = !_isBatteryWatched;
	//}

	//private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e) {
	//	BatteryStateLabel.Text = e.State switch {
	//		BatteryState.Charging => "Battery is currently charging",
	//		BatteryState.Discharging => "Charger is not connected and the battery is discharging",
	//		BatteryState.Full => "Battery is full",
	//		BatteryState.NotCharging => "The battery isn't charging.",
	//		BatteryState.NotPresent => "Battery is not available.",
	//		BatteryState.Unknown => "Battery is unknown",
	//		_ => "Battery is unknown"
	//	};

	//	BatteryLevelLabel.Text = $"Battery is {e.ChargeLevel * 100}% charged.";
	//}
}