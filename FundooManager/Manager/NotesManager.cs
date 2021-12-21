using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository notesRepository;
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        public async Task<NotesModel> AddNotes(NotesModel note)
        {
            try
            {
                return await this.notesRepository.AddNotes(note);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> UpdateNotes(NotesModel noteData, int noteId)
        {
            try
            {
                return await this.notesRepository.UpdateNotes(noteData,noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> ChangeColor(int noteId, string color)
        {
            try
            {
                return await this.notesRepository.ChangeColor(noteId, color);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> AddToBin(int noteId)
        {
            try
            {
                return await this.notesRepository.AddToBin(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> RestoreNote(int noteId)
        {
            try
            {
                return await this.notesRepository.RestoreNote(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteNoteForever(int noteId)
        {
            try
            {
                return await this.notesRepository.DeleteNoteForever(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> PinNote(int noteId)
        {
            try
            {
                return await this.notesRepository.PinNote(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> UnPinNote(int noteId)
        {
            try
            {
                return await this.notesRepository.UnPinNote(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> ArchiveNote(int noteId)
        {
            try
            {
                return await this.notesRepository.ArchiveNote(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<NotesModel> UnArchiveNote(int noteId)
        {
            try
            {
                return await this.notesRepository.UnArchiveNote(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<NotesModel>> GetAllNotes(int userId)
        {
            try
            {
                return  await this.notesRepository.GetAllNotes(userId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetAllArchives(int userId)
        {
            try
            {
                return await this.notesRepository.GetAllArchives(userId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<NotesModel>> GetAllBinNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetAllBinNotes(userId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetAllPinNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetAllPinNotes(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
