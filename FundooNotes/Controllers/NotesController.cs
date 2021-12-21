using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }

        [HttpPost]
        [Route("api/addNotes")]
        public async Task<IActionResult> AddNotes([FromBody] NotesModel notedata)
        {
            try
            {
                NotesModel data = await this.notesManager.AddNotes(notedata);
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
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpPut]
        [Route("api/update")]
        public async Task<IActionResult> UpdateNotes(NotesModel notedata, int noteId)
        {
            try
            {
                NotesModel data = await this.notesManager.UpdateNotes(notedata, noteId);
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
                return this.NotFound(new { Status = true, e.Message });
            }
        }


        [HttpPut]
        [Route("api/changeColor")]
        public async Task<IActionResult> ChangeColor(int noteId, string color)
        {
            try
            {
                NotesModel data = await this.notesManager.ChangeColor(noteId, color);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>{ Status = true, Message = "Color changed successfully" ,Data = data});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/addToBin")]
        public async Task<IActionResult> AddToBin(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.AddToBin(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel>{ Status = true, Message = "Note added to bin" ,Data = result});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/restore")]
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
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("api/delete")]
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
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/pin")]
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
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/unpin")]
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
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/archive")]
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
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/unarchive")]
        public async Task<IActionResult> UnArchiveNote(int noteId)
        {
            try
            {
                NotesModel result = await this.notesManager.UnArchiveNote(noteId);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Unarchived note successfully" , Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such note found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/allNotes")]
        public async Task<IActionResult> GetAllNotes(int userID)
        {
            try
            {
                var result = await this.notesManager.GetAllNotes(userID);
                if (result!=null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"UserID - {userID} not found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/allArchives")]
        public async Task<IActionResult> GetAllArchives(int userID)
        {
            try
            {
                var result = await this.notesManager.GetAllArchives(userID);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>>{ Status = true, Message = $"Retrieved all archived notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"UserID - {userID} not found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/allBinNotes")]
        public async Task<IActionResult> GetAllBinNotes(int userID)
        {
            try
            {
                var result = await this.notesManager.GetAllBinNotes(userID);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all bin notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"UserID - {userID} not found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/allPinNotes")]
        public async Task<IActionResult> GetAllPinNotes(int userID)
        {
            try
            {
                var result = await this.notesManager.GetAllPinNotes(userID);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<NotesModel>> { Status = true, Message = $"Retrieved all pinned notes successfully for UserID - {userID}", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"UserID - {userID} not found in our system" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
