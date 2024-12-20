namespace DCProyectoApuntes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.DCnotePage), typeof(Views.DCnotePage));
        }
    }
}
