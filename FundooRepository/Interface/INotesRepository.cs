using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface INotesRepository
    {
        NotesModel AddNotes(NotesModel noteData);
        bool ChangeColor(int noteId, string color);
    }
}
