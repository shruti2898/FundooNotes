using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class CollaboratorsRepository : ICollaboratorsRepository
    {
        private readonly UserContext context;
        public CollaboratorsRepository(UserContext context)
        {
            this.context = context;
        }
        public async Task<CollaboratorsModel> AddCollaborators(CollaboratorsModel collabData)
        {
            try
            { 
                if (collabData.CollaboratorsEmail != collabData.UserEmail)
                { 
                    await this.context.Collaborators.AddAsync(collabData);
                    await this.context.SaveChangesAsync();
                    return collabData;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
