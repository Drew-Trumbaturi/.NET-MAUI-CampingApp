using System;
using Microsoft.Maui.Controls;

namespace CampingApp.Pages;

public partial class PdfPage : ContentPage
{
	public PdfPage() {
		InitializeComponent();
		PdfWebView.Navigating += PdfWebView_Navigating;

		// Setting the initial URL for the webview
		PdfWebView.Source = new Uri("https://seasonedcitizenprepper.com/preparedness-downloads/");
	}


	private void PdfWebView_Navigating(object sender, WebNavigatingEventArgs e) {
		

		// Check if the URL being navigated to is a PDF document link (you can customize this check).
		if (IsPdfDocumentLink(e.Url)) {
			Console.WriteLine($"Navigating to: {e.Url}");
			// Load the PDF document from the clicked link.
			viewModel.SetPdfDocumentStream(e.Url);

			// Cancel the WebView navigation since you're handling it manually.
			e.Cancel = true;

			// Refresh the WebView to show the PDF link content.
			PdfWebView.Reload();
		}
	}

	private bool IsPdfDocumentLink(string url) {
		// Customize this logic to check if the URL points to a PDF document.
		// For example, you can check for file extensions like .pdf.
		// Return true if it's a PDF link; otherwise, return false.
		return url.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase);
	}
}