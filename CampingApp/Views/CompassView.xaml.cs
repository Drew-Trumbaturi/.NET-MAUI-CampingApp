namespace CampingApp.Views;
using Microsoft.Maui.Devices.Sensors;

public partial class CompassView 
{
	private CancellationTokenSource _cancelTokenSource;
	private bool _isCheckingLocation;

	public CompassView() {
		InitializeComponent();
	}

	private async void VerticalStackLayout_Loaded(object sender, EventArgs e) {
		// Call ToggleCompass method on page load
		ToggleCompass();
		ToggleBarometer();
		await GetCurrentLocation();
	}

	private void ToggleCompass() {
		if (Compass.Default.IsSupported) {
			if (!Compass.Default.IsMonitoring) {
				// Turn on compass
				Compass.Default.ReadingChanged += Compass_ReadingChanged;
				Compass.Default.Start(SensorSpeed.UI, applyLowPassFilter: true);
			} else {
				// Turn off compass
				Compass.Default.Stop();
				Compass.Default.ReadingChanged -= Compass_ReadingChanged;
			}
		}
	}

	private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e) {
		var data = e.Reading;
		
		// Update UI label with compass state
		CompassLabel.Text = $"Compass: {string.Format("{0:0}", data.HeadingMagneticNorth)}°";

		// Rotate the Compass background "dial" image
		this.Dispatcher.Dispatch(() => {
			CompassDial.RotateTo(-data.HeadingMagneticNorth, 50, Easing.SpringOut);
		});
	}

	public async Task GetCurrentLocation() {
		try {
			_isCheckingLocation = true;

			GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(.1));

			_cancelTokenSource = new CancellationTokenSource();

			Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

			if (location != null) {
				LatText.Text = $"Lat: {string.Format("{0:0.000}", location.Latitude)}°";
				LonText.Text = $"Lon: {string.Format("{0:0.000}", location.Longitude)}°";
				AltText.Text = $"Alt: {string.Format("{0:0}", location.Altitude)}m";
			}
		}
		// Catch one of the following exceptions:
		//   FeatureNotSupportedException
		//   FeatureNotEnabledException
		//   PermissionException
		catch (Exception ex) {
			// Unable to get location
		} finally {
			_isCheckingLocation = false;
		}
	}

	public void CancelRequest() {
		if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
			_cancelTokenSource.Cancel();
	}

	public void ToggleBarometer() {
		if (Barometer.Default.IsSupported) {
			if (!Barometer.Default.IsMonitoring) {
				// Turn on barometer
				Barometer.Default.ReadingChanged += Barometer_ReadingChanged;
				Barometer.Default.Start(SensorSpeed.UI);
			} else {
				// Turn off barometer
				Barometer.Default.Stop();
				Barometer.Default.ReadingChanged -= Barometer_ReadingChanged;
			}
		}
	}

	private void Barometer_ReadingChanged(object sender, BarometerChangedEventArgs e) {
		// 1/hPa = 0.0145/PSI
		// 1/hPa = 0.1/kPa
		// 1/hPa = 100/Pa

		// 1/hPa * 100/Pa / 3386 = 0.029in Hg
		var data = e.Reading;

		// Update UI Label with barometer state
		PresText.Text = $"Pressure: {string.Format("{0:0}", data.PressureInHectopascals)} hPa";
	}
}