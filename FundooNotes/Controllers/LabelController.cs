// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for Label
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    public class LabelController : ControllerBase
    {
        /// <summary>
        /// The label manager
        /// </summary>
        private readonly ILabelManager labelManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelManager">The label manager.</param>
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        /// <summary>
        /// Adds the labels on note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// Ok object result if label is added successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPost]
        [Route("api/addLabel")]
        public async Task<IActionResult> AddLabelsOnNote([FromBody] LabelModel labelData)
        {
            try
            {
                LabelModel data = await this.labelManager.AddLabelsOnNote(labelData);
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

        /// <summary>
        /// Creates the labels for user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// Ok object result if label is added successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPost]
        [Route("api/createLabel")]
        public async Task<IActionResult> CreateLabelsForUser([FromBody] LabelModel labelData)
        {
            try
            {
                LabelModel data = await this.labelManager.CreateLabelsForUser(labelData);
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

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// Ok object result if all labels are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
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

        /// <summary>
        /// Gets all notes from label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// Ok object result if all notes with same label name are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
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

        /// <summary>
        /// Deletes the user label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// Ok object result if label is deleted successfully from user
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpDelete]
        [Route("api/deleteLabel")]
        public async Task<IActionResult> DeleteUserLabel([FromBody] LabelModel labelData)
        {
            try
            {
                var result = await this.labelManager.DeleteUserLabel(labelData);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Label deleted from user successfully" });
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

        /// <summary>
        /// Removes the note label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// Ok object result if label is removed successfully from note
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
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

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// Ok object result if label name is edited successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
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
