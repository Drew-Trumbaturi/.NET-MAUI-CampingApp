using System.Reflection;
using System.ComponentModel;
using System.Windows.Input;

namespace CampingApp.ViewModel;

class NewPage1 : INotifyPropertyChanged {
	private Stream m_pdfDocumentStream;

	/// <summary>
	/// An event to detect the change in the value of a property.
	/// </summary>
	public event PropertyChangedEventHandler PropertyChanged;

	/// <summary>
	/// The PDF document stream that is loaded into the instance of the PDF viewer. 
	/// </summary>
	public Stream PdfDocumentStream {
		get {
			return m_pdfDocumentStream;
		}
		set {
			m_pdfDocumentStream = value;
			OnPropertyChanged("PdfDocumentStream");
		}
	}

	/// <summary>
	/// Constructor of the view model class
	/// </summary>
	public NewPage1() {
		//Accessing the PDF document that is added as embedded resource as stream.
	}

	/// <summary>
	/// Gets and sets the document stream from the given URL. 
	/// </summary>
	/// <param name="URL">Document URL</param>
	public async void SetPdfDocumentStream(string URL) {
		HttpClient httpClient = new HttpClient();
		HttpResponseMessage response = await httpClient.GetAsync(URL);

		PdfDocumentStream = await response.Content.ReadAsStreamAsync();
	}

	public void OnPropertyChanged(string name) {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}