using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DCProyectoApuntes.Models
{
    internal class DCNote
    {
        public string DCFilename { get; set; }
        public string DCText { get; set; }
        public DateTime DCDate { get; set; }

        public DCNote()
        {
            DCFilename = $"{Path.GetRandomFileName()}.notes.txt";
            DCDate = DateTime.Now;
            DCText = "";
        }
        public void DCSave() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, DCFilename), DCText);

        public void DCDelete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, DCFilename));

        public static DCNote DCLoad(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage.", filename);

            return
                new()
                {
                    DCFilename = Path.GetFileName(filename),
                    DCText = File.ReadAllText(filename),
                    DCDate = File.GetLastWriteTime(filename)
                };
        }

        public static IEnumerable<DCNote> DCLoadAll()
        {
            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            return Directory

                    // Select the file names from the directory
                    .EnumerateFiles(appDataPath, "*.notes.txt")

                    // Each file name is used to load a note
                    .Select(filename => DCNote.DCLoad(Path.GetFileName(filename)))

                    // With the final collection of notes, order them by date
                    .OrderByDescending(note => note.DCDate);
        }


    }


}
