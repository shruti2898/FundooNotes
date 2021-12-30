// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Notes Manager Class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.INotesManager" />
    public class NotesManager : INotesManager
    {
        /// <summary>
        /// The notes repository
        /// </summary>
        private readonly INotesRepository notesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager"/> class.
        /// </summary>
        /// <param name="notesRepository">The notes repository.</param>
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <returns>Data of newly added note</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// Data of updated note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> UpdateNotes(NotesModel noteData)
        {
            try
            {
                return await this.notesRepository.UpdateNotes(noteData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// Note data after changing the note color
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Adds to bin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after adding note to bin
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after restoring the note from bin
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Deletes the note forever.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// True if note is deleted successfully else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after pinning the note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Unpin the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after unpinning the note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after archiving the note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Removing the note from archives.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after removing note from archives
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of all notes created by user</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<NotesModel>> GetAllNotes(int userId)
        {
            try
            {
                return await this.notesRepository.GetAllNotes(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Gets all archives.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of all archived notes</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Gets all bin notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of all bin notes</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Gets all pin notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of all pinned notes</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Adds reminder on note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>
        /// Note data after adding reminder on note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> Reminder(int noteId, string reminder)
        {
            try
            {
                return await this.notesRepository.Reminder(noteId, reminder);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after removing the reminder from note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> DeleteReminder(int noteId)
        {
            try
            {
                return await this.notesRepository.DeleteReminder(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Gets all reminders.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of reminders added by user</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<NotesModel>> GetAllReminders(int userId)
        {
            try
            {
                return await this.notesRepository.GetAllReminders(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// Note data after adding image on note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> AddImage(int noteId, IFormFile image)
        {
            try
            {
                return await this.notesRepository.AddImage(noteId, image);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Note data after deleting image from the note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> DeleteImage(int noteId)
        {
            try
            {
                return await this.notesRepository.DeleteImage(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
