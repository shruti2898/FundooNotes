// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for Notes
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        /// <summary>
        /// The notes manager
        /// </summary>
        private readonly INotesManager notesManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesManager">The notes manager.</param>
        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// Ok object result if note is added successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPost]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel noteData)
        {
            try
            {
                noteData.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
                NotesModel data = await this.notesManager.AddNotes(noteData);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Note Created Successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to create note. Please provide required data for the note." });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, e.Message });
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="noteData">The note data.</param>
        /// <returns>
        /// Ok object result if note is updated successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        public async Task<IActionResult> UpdateNotes([FromBody] NotesModel noteData)
        {
            try
            {
                noteData.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
                NotesModel data = await this.notesManager.UpdateNotes(noteData);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Note Updated Successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to update note. Please provide required data for the note." });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, e.Message });
            }
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// Ok object result if note color is changed successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/color")]
        public async Task<IActionResult> ChangeColor(int noteId,[FromBody] NotesModel noteData)
        {
            try
            {
                noteData.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
                NotesModel data = await this.notesManager.ChangeColor(noteId,noteData);  
                if (data != null)
                {
                    var result = new
                    {
                        noteId = data.NotesId,
                        noteColor = data.NoteColor
                    };
                    return this.Ok(new { Status = true, Message = "Color changed successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Adds to bin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is added to bin successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/remove")]
        public async Task<IActionResult> AddToBin(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.AddToBin(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Note added to bin", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Restores the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is restored from bin successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/restore")]
        public async Task<IActionResult> RestoreNote(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.RestoreNote(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Note restored from bin successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the note forever.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is deleted successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpDelete]
        [Route("{noteId}/delete")]
        public async Task<IActionResult> DeleteNoteForever(int noteId)
        {
            try
            {
                bool result = await this.notesManager.DeleteNoteForever(noteId);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Note deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("empty")]
        public async Task<IActionResult> EmptyBin()
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
                bool result = await this.notesManager.EmptyBin(Convert.ToInt32(userID));
                if (result)
                {
                    return this.Ok(new { Status = true, Message = $"Deleted all notes from bin successfully"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Pins the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is pinned successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/pin")]
        public async Task<IActionResult> PinNote(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.PinNote(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Pinned note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Unpin the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is unpinned successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/unpin")]
        public async Task<IActionResult> UnPinNote(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.UnPinNote(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Unpinned note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Archives the note.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is archived successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/archive")]
        public async Task<IActionResult> ArchiveNote(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.ArchiveNote(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Archived note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Removes the note from archives.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if note is removed from archives successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/unarchive")]
        public async Task<IActionResult> UnArchiveNote(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.UnArchiveNote(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Unarchived note successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// Ok object result if all notes are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {   
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
                var result = await this.notesManager.GetAllNotes(Convert.ToInt32(userID));
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all archives.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// Ok object result if all archived notes are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpGet]
        [Route("archives")]
        public async Task<IActionResult> GetAllArchives()
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
                var result = await this.notesManager.GetAllArchives(Convert.ToInt32(userID));
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all archived notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all bin notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// Ok object result if all bin notes are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpGet]
        [Route("trash")]
        public async Task<IActionResult> GetAllBinNotes()
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
                var result = await this.notesManager.GetAllBinNotes(Convert.ToInt32(userID));
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all bin notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

       

        /// <summary>
        /// Gets all pin notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param> 
        /// <returns>
        /// Ok object result if all pin notes are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpGet]
        [Route("pin")]
        public async Task<IActionResult> GetAllPinNotes()
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
                var result = await this.notesManager.GetAllPinNotes(Convert.ToInt32(userID));
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all pinned notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Reminders the specified note identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="reminder">The reminder.</param>
        /// <returns>
        /// Ok object result if reminder is added on note successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/reminder")]
        public async Task<IActionResult> Reminder(int noteId,[FromBody] NotesModel noteData)
        {
            try
            {
                noteData.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value);
                NotesModel result = await this.notesManager.Reminder(noteId, noteData);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Reminder added successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the reminder.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if reminder is removed from note successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/removeReminder")]
        public async Task<IActionResult> DeleteReminder(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.DeleteReminder(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Reminder removed successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Gets all reminders.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// Ok object result if all notes with reminder are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpGet]
        [Route("reminders")]
        public async Task<IActionResult> GetAllReminders()
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == "UserId").Value;
                var result = await this.notesManager.GetAllReminders(Convert.ToInt32(userID));
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all reminders successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <param name="image">The image.</param>
        /// <returns>
        /// Ok object result if image is added on note successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/image")]
        public async Task<IActionResult> AddImage(int noteId, IFormFile image)
        {
            try
            {
                NotesModel result = await this.notesManager.AddImage(noteId, image);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Image added successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the image.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if image is deleted from successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as bad request object result</exception>
        [HttpPut]
        [Route("{noteId}/deleteImage")]
        public async Task<IActionResult> DeleteImage(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.DeleteImage(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Image removed successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Status = false, Message = ex.Message });
            }
        }
    }
}
