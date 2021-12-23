﻿using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly UserContext context;
        public NotesRepository(UserContext context)
        {
            this.context = context;
        }
        public async Task<NotesModel> AddNotes(NotesModel noteData)
        {
            try
            {
                bool checkNullVaules = (noteData.NoteTitle == null && noteData.NoteDescription == null && noteData.NoteReminder == null && noteData.AddImage == null);

                if (!checkNullVaules)
                {
                    await this.context.Notes.AddAsync(noteData);
                    await this.context.SaveChangesAsync();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> UpdateNotes(NotesModel noteData, int noteId)
        {
            try
            {
                NotesModel result = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                bool checkNullVaules = (noteData.NoteTitle == null || noteData.NoteDescription == null || noteData.NoteReminder == null || noteData.AddImage == null);

                if (result != null && !checkNullVaules)
                {
                    result.NoteTitle = noteData.NoteTitle;
                    result.NoteDescription = noteData.NoteDescription;
                    result.NoteReminder = noteData.NoteReminder;
                    result.NoteColor = noteData.NoteColor;
                    result.AddImage = noteData.AddImage;
                    result.ArchiveNote = noteData.ArchiveNote;
                    result.DeleteNote = noteData.DeleteNote;
                    result.PinNote = noteData.PinNote;

                    this.context.Notes.Update(result);
                    await this.context.SaveChangesAsync();
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> ChangeColor(int noteId, string color)
        {
            try
            {
                var noteData =  await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (noteData!=null && color != null)
                {
                    noteData.NoteColor = color;
                    this.context.Notes.Update(noteData);
                    await this.context.SaveChangesAsync();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> AddToBin(int noteId)
        {
            try
            {
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && !data.DeleteNote);
                if (noteData != null)
                {
                    noteData.DeleteNote = true;
                    noteData.ArchiveNote = false;
                    noteData.PinNote = false;
                    this.context.Notes.Update(noteData);
                    this.context.SaveChanges();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> RestoreNote(int noteId)
        {
            try
            {
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && data.DeleteNote);
                if (noteData != null)
                {
                    noteData.DeleteNote = false;
                    this.context.Notes.Update(noteData);
                    this.context.SaveChanges();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteNoteForever(int noteId)
        {
            try
            {
                var noteData = await this .context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (noteData != null)
                {
                    this.context.Notes.Remove(noteData);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> PinNote(int noteId)
        {
            try
            {
                var noteData =await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && !data.PinNote && !data.DeleteNote);
                if (noteData != null )
                {
                    noteData.PinNote = true;
                    noteData.ArchiveNote = false;
                    this.context.Notes.Update(noteData);
                    await this.context.SaveChangesAsync();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> UnPinNote(int noteId)
        {
            try
            {
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && !data.DeleteNote);
                if (noteData != null)
                {
                    noteData.PinNote = false;
                    noteData.ArchiveNote = false;
                    this.context.Notes.Update(noteData);
                    await this.context.SaveChangesAsync();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> ArchiveNote(int noteId)
        {
            try
            {
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId &&  !data.DeleteNote);
                if (noteData != null)
                {
                    noteData.ArchiveNote = true;
                    noteData.PinNote = false;
                    this.context.Notes.Update(noteData);
                    await this.context.SaveChangesAsync();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<NotesModel> UnArchiveNote(int noteId)
        {
            try
            {   
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && !data.DeleteNote);
                if (noteData != null)
                {
                    noteData.ArchiveNote = false;
                    noteData.PinNote = false;
                    this.context.Notes.Update(noteData);
                    await this.context.SaveChangesAsync();
                    return noteData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetAllNotes(int userID)
        {
            try
            {
                var notes = await this.context.Notes.Where(data => data.UserId == userID && !data.DeleteNote && !data.ArchiveNote).ToListAsync();
                if(notes.Count > 0)
                {
                    return notes;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetAllArchives(int userID)
        {
            try
            {
                var archives = await this.context.Notes.Where(data => data.UserId == userID && data.ArchiveNote && !data.DeleteNote && !data.PinNote).ToListAsync();
                if (archives.Count > 0)
                {
                    return archives;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetAllBinNotes(int userID)
        {
            try
            {
                var binNotes = await this.context.Notes.Where(data => data.UserId == userID && data.DeleteNote && !data.ArchiveNote && !data.PinNote).ToListAsync();
                if (binNotes.Count > 0)
                {
                    return binNotes;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<NotesModel>> GetAllPinNotes(int userID)
        {
            try
            {
                var pinNotes = await this.context.Notes.Where(data => data.UserId == userID && data.PinNote && !data.ArchiveNote && !data.DeleteNote).ToListAsync();
                if (pinNotes.Count > 0)
                {
                    return pinNotes;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}