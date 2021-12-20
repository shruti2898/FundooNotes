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
        public IActionResult AddNotes([FromBody] NotesModel notedata)
        {
            try
            {
                NotesModel data = this.notesManager.AddNotes(notedata);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<NotesModel> { Status = true, Message = "Added Successfully", Data = notedata });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpPut]
        [Route("api/changeColor")]
        public IActionResult ChangeColor(int noteId, string color)
        {
            try
            {
                bool result = this.notesManager.ChangeColor(noteId, color);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Changed color successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
