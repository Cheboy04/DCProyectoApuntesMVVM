using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace DCProyectoApuntes.ViewModels
{
    internal class DCNoteViewModel : ObservableObject, IQueryAttributable
    {
        private Models.DCNote _note;

        public string DCText
        {
            get => _note.DCText;
            set
            {
                if (_note.DCText != value)
                {
                    _note.DCText = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DCDate => _note.DCDate;

        public string Identifier => _note.DCFilename;

        public ICommand DCSaveCommand { get; private set; }
        public ICommand DCDeleteCommand { get; private set; }

        public DCNoteViewModel()
        {
            _note = new Models.DCNote();
            DCSaveCommand = new AsyncRelayCommand(DCSave);
            DCDeleteCommand = new AsyncRelayCommand(DCDelete);
        }

        public DCNoteViewModel(Models.DCNote note)
        {
            _note = note;
            DCSaveCommand = new AsyncRelayCommand(DCSave);
            DCDeleteCommand = new AsyncRelayCommand(DCDelete);
        }

        private async Task DCSave()
        {
            _note.DCDate = DateTime.Now;
            _note.DCSave();
            await Shell.Current.GoToAsync($"..?saved={_note.DCFilename}");
        }

        private async Task DCDelete()
        {
            _note.DCDelete();
            await Shell.Current.GoToAsync($"..?deleted={_note.DCFilename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Models.DCNote.DCLoad(query["load"].ToString());
                DCRefreshProperties();
            }
        }

        public void DCReload()
        {
            _note = Models.DCNote.DCLoad(_note.DCFilename);
            DCRefreshProperties();
        }

        private void DCRefreshProperties()
        {
            OnPropertyChanged(nameof(DCText));
            OnPropertyChanged(nameof(DCDate));
        }
    }

}
