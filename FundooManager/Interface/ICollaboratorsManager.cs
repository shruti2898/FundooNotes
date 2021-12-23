using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface ICollaboratorsManager
    {
        Task<CollaboratorsModel> AddCollaborators(CollaboratorsModel collabData);
    }
}
