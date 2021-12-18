using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext noteContext;
        public NotesRepository(UserContext noteContext)
        {
            this.noteContext = noteContext;
        }
    }
}
