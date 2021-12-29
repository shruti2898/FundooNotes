using FundooModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        Task<NotesModel> AddNotes(NotesModel noteData);
        Task<NotesModel> UpdateNotes(NotesModel noteData);
        Task<IEnumerable<NotesModel>> GetAllNotes(int userID);
        Task<NotesModel> ChangeColor(int noteId, string color);
        Task<NotesModel> AddToBin(int noteId);
        Task<IEnumerable<NotesModel>> GetAllBinNotes(int userID);
        Task<NotesModel> RestoreNote(int noteId);
        Task<bool> DeleteNoteForever(int noteId);
        Task<NotesModel> PinNote(int noteId);
        Task<IEnumerable<NotesModel>> GetAllPinNotes(int userID);
        Task<NotesModel> UnPinNote(int noteId);
        Task<NotesModel> ArchiveNote(int noteId);
        Task<NotesModel> UnArchiveNote(int noteId);
        Task<IEnumerable<NotesModel>> GetAllArchives(int userID);
        Task<NotesModel> Reminder(int noteId, string reminder);
        Task<NotesModel> DeleteReminder(int noteId);
        Task<IEnumerable<NotesModel>> GetAllReminders(int userID);
        Task<NotesModel> AddImage(int noteId, IFormFile image);
        Task<NotesModel> DeleteImage(int noteId);
    }
}
