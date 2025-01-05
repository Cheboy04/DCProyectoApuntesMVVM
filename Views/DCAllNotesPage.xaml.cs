namespace DCProyectoApuntes.Views;

public partial class DCAllNotesPage : ContentPage
{
	public DCAllNotesPage()
	{
		InitializeComponent();

    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        notesCollection.SelectedItem = null;
    }
}