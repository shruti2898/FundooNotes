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
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorsManager collabManager;
        public CollaboratorController(ICollaboratorsManager collabManager)
        {
            this.collabManager = collabManager;
        }

        [HttpPost]
        [Route("api/addCollab")]
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
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpDelete]
        [Route("api/deleteCollab")]
        public async Task<IActionResult> DeleteCollaborator([FromBody] CollaboratorsModel collabDetails)
        {
            try
            {
                var result = await this.collabManager.DeleteCollaborator(collabDetails);
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
                return this.NotFound(new { Status = true, e.Message });
            }
        }

        [HttpGet]
        [Route("api/allCollabs")]
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
                return this.NotFound(new { Status = true, e.Message });
            }
        }
    }
}
