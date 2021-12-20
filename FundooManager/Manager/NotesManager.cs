using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository notesRepository;
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        public NotesModel AddNotes(NotesModel note)
        {
            try
            {
                return this.notesRepository.AddNotes(note);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public bool ChangeColor(int noteId, string color)
        {
            try
            {
                return this.notesRepository.ChangeColor(noteId, color);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
