using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DCProyectoApuntes.ViewModels
{
    internal class DCNotesViewModel : IQueryAttributable
    {
        public ObservableCollection<DCNoteViewModel> DCAllNotes { get; }
        public ICommand DCNewCommand { get; }
        public ICommand DCSelectNoteCommand { get; }

        public DCNotesViewModel()
        {
            DCAllNotes = new ObservableCollection<DCNoteViewModel>(Models.DCNote.DCLoadAll().Select(n => new DCNoteViewModel(n)));
            DCNewCommand = new AsyncRelayCommand(DCNewNoteAsync);
            DCSelectNoteCommand = new AsyncRelayCommand<DCNoteViewModel>(DCSelectNoteAsync);
        }

        private async Task DCNewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.DCnotePage));
        }

        private async Task DCSelectNoteAsync(DCNoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.DCnotePage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                DCNoteViewModel matchedNote = DCAllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note exists, delete it
                if (matchedNote != null)
                    DCAllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                DCNoteViewModel matchedNote = DCAllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.DCReload();
                    DCAllNotes.Move(DCAllNotes.IndexOf(matchedNote), 0);
                }

                // If note isn't found, it's new; add it.
                else
                    DCAllNotes.Insert(0, new DCNoteViewModel(Models.DCNote.DCLoad(noteId)));
            }
        }
    }
}
