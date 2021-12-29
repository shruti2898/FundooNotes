using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorsManager
    {
        Task<CollaboratorsModel> AddCollaborator(CollaboratorsModel collabData);
        Task<bool> DeleteCollaborator(CollaboratorsModel collabData);
        Task<IEnumerable<CollaboratorsModel>> GetAllCollaborators(int noteId);
    }
}
