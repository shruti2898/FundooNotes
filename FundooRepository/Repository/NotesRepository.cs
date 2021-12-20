using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public NotesModel AddNotes(NotesModel noteData)
        {
            try
            {
                bool checkNullVaules = (noteData.NoteTitle != null || noteData.NoteDescription != null || noteData.NoteReminder != null || noteData.AddImage != null);

                bool[] boolArray = new bool[]{ noteData.PinNote, noteData.ArchiveNote, noteData.DeleteNote };
                int count = 0;
                foreach (var item in boolArray)
                {
                    if (item == false) count++;
                }

                if (count>1 && checkNullVaules)
                {
                    this.noteContext.Notes.Add(noteData);
                    this.noteContext.SaveChanges();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ChangeColor(int noteId, string color)
        {
            try
            {
                var noteData = this.noteContext.Notes.SingleOrDefault(data => data.NotesId == noteId);
                if (color != null)
                {
                    noteData.NoteColor = color;
                    this.noteContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
