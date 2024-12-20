namespace DCProyectoApuntes.Views;

public partial class DCaboutPage : ContentPage
{
	public DCaboutPage()
	{
        InitializeComponent();
	}
    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.DCAbout about)
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(about.MoreInfoUrl);
    }
}