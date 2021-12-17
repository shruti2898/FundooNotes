using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class NotesManager
    {
        private readonly INotesRepository notesRepository;
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }
    }
}
