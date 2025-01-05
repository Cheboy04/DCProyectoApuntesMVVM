using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DCProyectoApuntes.ViewModels
{
    internal class DCAboutViewModel
    {
        public string DCTitle => AppInfo.Name;
        public string DCVersion => AppInfo.VersionString;
        public string DCMoreInfoUrl => "https://aka.ms/maui";
        public string DCMessage => "This app is written in XAML and C# with .NET MAUI.";
        public ICommand DCShowMoreInfoCommand { get; }

        public DCAboutViewModel()
        {
            DCShowMoreInfoCommand = new AsyncRelayCommand(DCShowMoreInfo);
        }

        async Task DCShowMoreInfo() =>
            await Launcher.Default.OpenAsync(DCMoreInfoUrl);
    }
}
