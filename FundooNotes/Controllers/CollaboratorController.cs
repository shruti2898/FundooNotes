// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorController.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooNotes.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for Collaborator
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
   
    public class CollaboratorController : ControllerBase
    {
        /// <summary>
        /// The collaborator manager
        /// </summary>
        private readonly ICollaboratorsManager collabManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="collabManager">The collaborator manager.</param>
        public CollaboratorController(ICollaboratorsManager collabManager)
        {
            this.collabManager = collabManager;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabDetails">The collaborator details.</param>
        /// <returns>
        /// Ok object result if collaborator added successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpPost]
        [Route("addCollab")]
        public async Task<IActionResult> AddCollaborator([FromBody] CollaboratorsModel collabDetails)
        {
            try
            {
                CollaboratorsModel data = await this.collabManager.AddCollaborator(collabDetails);
                if (data != null)
                {
                    return this.Ok(new ResponseModel<CollaboratorsModel> { Status = true, Message = "Collaborators Added Successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Unable to add collaborator." });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, e.Message });
            }
        }

        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="collabDetails">The collaborator details.</param>
        /// <returns>
        /// Ok object result if collaborator deleted successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpDelete]
        [Route("deleteCollab/{collabId}")]
        public async Task<IActionResult> DeleteCollaborator(int collabId)
        {
            try
            {
                var result = await this.collabManager.DeleteCollaborator(collabId);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Collaborator Removed Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Collaborator does not exist" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, e.Message });
            }
        }

        /// <summary>
        /// Gets all collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// Ok object result if all collaborators are retrieved successfully
        /// else bad request object result
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message as not found object result</exception>
        [HttpGet]
        [Route("{noteId}")]
        public async Task<IActionResult> GetAllCollaborators(int noteId)
        {
            try
            {
                var data = await this.collabManager.GetAllCollaborators(noteId);
                if (data != null)
                {
                    return this.Ok(new { Status = true, Message = "Retrieved all collaborators successfully", Data = data });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "No data found in our system" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, e.Message });
            }
        }
    }
}
