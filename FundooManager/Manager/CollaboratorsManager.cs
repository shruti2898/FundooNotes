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

        public async Task<CollaboratorsModel> AddCollaborator(CollaboratorsModel collabData)
        {
            try
            {
                return await this.collabRepository.AddCollaborator(collabData);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<bool> DeleteCollaborator(CollaboratorsModel collabData)
        {
            try
            {
                return await this.collabRepository.DeleteCollaborator(collabData);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<CollaboratorsModel>> GetAllCollaborators(int noteId)
        {
            try
            {
                return await this.collabRepository.GetAllCollaborators(noteId);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
