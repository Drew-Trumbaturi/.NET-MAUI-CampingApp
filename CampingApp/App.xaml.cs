namespace CampingApp
{
	public partial class App : Application
	{
		public App() {
			InitializeComponent();
			if (DeviceInfo.Idiom == DeviceIdiom.Phone)
				Shell.Current.CurrentItem = PhoneTabs;
		}

		

		private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {

		}
	}
}