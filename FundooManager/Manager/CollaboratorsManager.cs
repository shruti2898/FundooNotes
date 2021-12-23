using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class CollaboratorsManager : ICollaboratorsManager
    {
        private readonly ICollaboratorsRepository collabRepository;
        public CollaboratorsManager(ICollaboratorsRepository collabRepository)
        {
            this.collabRepository = collabRepository;
        }

        public async Task<CollaboratorsModel> AddCollaborators(CollaboratorsModel collabData)
        {
            try
            {
                return await this.collabRepository.AddCollaborators(collabData);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
