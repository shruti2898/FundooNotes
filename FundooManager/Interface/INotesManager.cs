using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        NotesModel AddNotes(NotesModel noteData);
        bool ChangeColor(int noteId, string color);
    }
}
