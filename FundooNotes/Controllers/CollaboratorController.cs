using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorsManager collabManager;
        public CollaboratorController(ICollaboratorsManager collabManager)
        {
            this.collabManager = collabManager;
        }
        [HttpPost]
        [Route("api/addCollaborator")]
        public async Task<IActionResult> AddCollaborators([FromBody] CollaboratorsModel collabData)
        {
            try
            {
                CollaboratorsModel data = await this.collabManager.AddCollaborators(collabData);
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
    }
}
