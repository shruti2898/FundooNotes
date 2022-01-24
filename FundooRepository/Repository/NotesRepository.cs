// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Notes Repository Interface
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.INotesRepository" />
    public class NotesRepository : INotesRepository
    {
        /// <summary>
        /// The context for Notes
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configuration">The configuration.</param>
        public NotesRepository(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// Data of newly added note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> AddNotes(NotesModel noteData)
        {
            try
            {
                bool checkNullVaules = noteData.NoteTitle == null && noteData.NoteDescription == null && noteData.NoteReminder == null && noteData.AddImage == null;
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
                NotesModel result = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteData.NotesId);
                bool checkNullVaules = noteData.NoteTitle == null && noteData.NoteDescription == null;
                if (result != null && !checkNullVaules)
                {
                    result.NoteTitle = noteData.NoteTitle;
                    result.NoteDescription = noteData.NoteDescription;
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

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// Note data after changing the note color
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> ChangeColor(int noteId,NotesModel noteData)
        {
            try
            {
                var data = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (data != null)
                {
                    data.NoteColor = noteData.NoteColor;
                    this.context.Notes.Update(data);
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
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
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

        public async Task<bool> EmptyBin(int userID)
        {
            try
            {
                var notes = this.context.Notes.Where(data => data.UserId == userID && data.DeleteNote).ToList();
                if (notes.Count > 0)
                {
                    this.context.Notes.RemoveRange(notes);
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
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && !data.PinNote && !data.DeleteNote);
                if (noteData != null)
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
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId && !data.DeleteNote);
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

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// List of all notes created by user
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<NotesModel>> GetAllNotes(int userID)
        {
            try
            {
                var notes = await this.context.Notes.Where(data => data.UserId == userID && !data.DeleteNote && !data.ArchiveNote).ToListAsync();
                if (notes.Count > 0)
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

        /// <summary>
        /// Gets all archives.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// List of all archived notes
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Gets all bin notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// List of all notes present in bin of a user
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Gets all pin notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// List of all pinned notes
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
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

        /// <summary>
        /// Adds reminder on note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>
        /// Note data after adding reminder on note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<NotesModel> Reminder(int noteId, NotesModel noteData)
        {
            try
            {
                var result = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (result != null)
                {
                    result.NoteReminder = noteData.NoteReminder;
                    this.context.Notes.Update(result);
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
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (noteData != null)
                {
                    noteData.NoteReminder = null;
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

        /// <summary>
        /// Gets all reminders.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// List of reminder notes of user
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<NotesModel>> GetAllReminders(int userID)
        {
            try
            {
                var reminders = await this.context.Notes.Where(data => data.UserId == userID && data.NoteReminder != null).ToListAsync();
                if (reminders.Count > 0)
                {
                    return reminders;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (noteData != null)
                {
                    string cloudName = this.Configuration.GetValue<string>("Cloudinary:CloudName");
                    string cloudApiKey = this.Configuration.GetValue<string>("Cloudinary:CloudApiKey");
                    string cloudApiSecret = this.Configuration.GetValue<string>("Cloudinary:CloudApiSecret");
                    var cloudinary = new Cloudinary(new Account(cloudName, cloudApiKey, cloudApiSecret));
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    var imageUrl = uploadResult.Url.ToString();
                    noteData.AddImage = imageUrl;
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
                var noteData = await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == noteId);
                if (noteData != null)
                {
                    noteData.AddImage = null;
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

      
    }
}
