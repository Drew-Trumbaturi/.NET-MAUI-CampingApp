namespace CampingApp.Views;
using Microsoft.Maui.Devices.Sensors;

public partial class StepCounterView
{
	int stepCount = 0;
	double prevGyroscopeValue = 0;

	public StepCounterView()
	{
		InitializeComponent();
		
		if (Accelerometer.Default.IsSupported) {
			if (!Accelerometer.Default.IsMonitoring) {
				// Turn on accelerometer
				Accelerometer.Default.ReadingChanged += Accelerometer_ReadingChanged;
				Accelerometer.Default.Start(SensorSpeed.UI);
			} else {
				// Turn off accelerometer
				Accelerometer.Default.Stop();
				Accelerometer.Default.ReadingChanged -= Accelerometer_ReadingChanged;
			}
		}

		if (Gyroscope.Default.IsSupported) {
			if (!Gyroscope.Default.IsMonitoring) {
				// Turn on gyroscope
				Gyroscope.Default.ReadingChanged += Gyroscope_ReadingChanged;
				Gyroscope.Default.Start(SensorSpeed.UI);
			} else {
				// Turn off gyroscope
				Gyroscope.Default.Stop();
				Gyroscope.Default.ReadingChanged -= Gyroscope_ReadingChanged;
			}
		}
	}

	//public void ToggleAccelerometer() {
		
	//}

	private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e) {
		int currentStepCount = stepCount;
		var data = e.Reading;

		// Calculate the magnitude of acceleration vector
		var magnitude = Math.Sqrt(data.Acceleration.X * data.Acceleration.X + data.Acceleration.Y * data.Acceleration.Y + data.Acceleration.Z * data.Acceleration.Z);

		// Update UI Label with accelerometer state
		StepLabel.TextColor = Colors.Green;
		//StepLabel.Text = $"Step Count: {currentStepCount}";

		// Assuming a step is taken when the acceleration crosses a certain threshold
		if (magnitude > 1.5) // Adjust the threshold as needed
		{
			CheckStep();
		}
	}

	void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e) {
		var data = e.Reading;

		// Detect steps based on the gyroscope data
		if (Math.Abs(data.AngularVelocity.Z - prevGyroscopeValue) > 2) {
			CheckStep();
		}

		prevGyroscopeValue = data.AngularVelocity.Z;
	}

	void CheckStep() {
		stepCount++;
		Device.BeginInvokeOnMainThread(() =>
		{
			StepLabel.Text = $"Step Count: {stepCount}";
		});
	}

	private void VerticalStackLayout_Loaded(object sender, EventArgs e) {
		//ToggleAccelerometer();
	}

	private void Button_Clicked(object sender, EventArgs e) {
		stepCount = 0;
	}
}