// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Notes Repository Interface
    /// </summary>
    public interface INotesRepository
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>Data of newly added note</returns>
        Task<NotesModel> AddNotes(NotesModel noteData);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>Data of updated note</returns>
        Task<NotesModel> UpdateNotes(NotesModel noteData);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>List of all notes created by user</returns>
        Task<IEnumerable<NotesModel>> GetAllNotes(int userID);

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>Note data after changing the note color</returns>
        Task<NotesModel> ChangeColor(int noteId, string color);

        /// <summary>
        /// Adds to bin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after adding note to bin</returns>
        Task<NotesModel> AddToBin(int noteId);

        /// <summary>
        /// Gets all bin notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>List of all notes present in bin of a user</returns>
        Task<IEnumerable<NotesModel>> GetAllBinNotes(int userID);

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after restoring the note from bin</returns>
        Task<NotesModel> RestoreNote(int noteId);

        /// <summary>
        /// Deletes the note forever.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>True if note is deleted successfully else false</returns>
        Task<bool> DeleteNoteForever(int noteId);

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after pinning the note</returns>
        Task<NotesModel> PinNote(int noteId);

        /// <summary>
        /// Gets all pin notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>List of all pinned notes</returns>
        Task<IEnumerable<NotesModel>> GetAllPinNotes(int userID);

        /// <summary>
        /// Unpin the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after unpinning the note</returns>
        Task<NotesModel> UnPinNote(int noteId);

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after archiving the note</returns>
        Task<NotesModel> ArchiveNote(int noteId);

        /// <summary>
        /// Removing the note from archives.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after removing note from archives</returns>
        Task<NotesModel> UnArchiveNote(int noteId);

        /// <summary>
        /// Gets all archives.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>List of all archived notes</returns>
        Task<IEnumerable<NotesModel>> GetAllArchives(int userID);

        /// <summary>
        /// Adds reminder on note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Note data after adding reminder on note</returns>
        Task<NotesModel> Reminder(int noteId, string reminder);

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after removing the reminder from note</returns>
        Task<NotesModel> DeleteReminder(int noteId);

        /// <summary>
        /// Gets all reminders.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>List of reminder notes of user</returns>
        Task<IEnumerable<NotesModel>> GetAllReminders(int userID);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>Note data after adding image on note</returns>
        Task<NotesModel> AddImage(int noteId, IFormFile image);

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>Note data after deleting image from the note</returns>
        Task<NotesModel> DeleteImage(int noteId);
    }
}
