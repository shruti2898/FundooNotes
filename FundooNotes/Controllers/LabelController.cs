using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        [HttpPost]
        [Route("api/addLabel")]
        public async Task<IActionResult> AddLabels([FromBody] LabelModel labelData)
        {
            try
            {
                LabelModel data = await this.labelManager.AddLabels(labelData);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<LabelModel> { Status = true, Message = "Label added successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to add label." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpGet]
        [Route("api/getLabels")]
        public async Task<IActionResult> GetAllLabels(int userId)
        {
            try
            {
                var data = await this.labelManager.GetAllLabels(userId);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<IEnumerable<LabelModel>> { Status = true, Message = "Retrieved all labels successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such data found in our system" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpGet]
        [Route("api/getLabelNotes")]
        public async Task<IActionResult> GetAllNotesFromLabel(int labelId)
        {
            try
            {
                var data = await this.labelManager.GetAllNotesFromLabel(labelId);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<List<NotesModel>> { Status = true, Message = "Retrieved all notes from label successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such data found in our system" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteLabel")]
        public async Task<IActionResult> DeleteUserLabel([FromBody] LabelModel labelData)
        {
            try
            {
                var result = await this.labelManager.DeleteUserLabel(labelData);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Label deleted from user successfully"});
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such data found in our system" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpDelete]
        [Route("api/removeLabel")]
        public async Task<IActionResult> RemoveNoteLabel(int labelId)
        {
            try
            {
                var result = await this.labelManager.RemoveNoteLabel(labelId);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Label removed from note successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such data found in our system" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpPut]
        [Route("api/editLabel")]
        public async Task<IActionResult> EditLabel([FromBody] LabelModel labelData)
        {
            try
            {
                var result = await this.labelManager.EditLabel(labelData);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Label edited successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No such data found in our system" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = true, e.Message });
            }
        }
    }
}
