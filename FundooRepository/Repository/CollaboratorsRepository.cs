// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooRepository.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Collaborators Repository Class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ICollaboratorsRepository" />
    public class CollaboratorsRepository : ICollaboratorsRepository
    {
        /// <summary>
        /// The context for collaborator
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CollaboratorsRepository(UserContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabData">The collaborator data.</param>
        /// <returns>
        /// Collaborator Model Data
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
       
        public async Task<CollaboratorsModel> AddCollaborator(CollaboratorsModel collabData)
        {
            try
            {
                var checkOwnerEmail = await this.context.Notes.SingleOrDefaultAsync(data => data.UserId == this.context.Users.Where(user => user.Email == collabData.CollabEmail).Select(owner => owner.UserId).SingleOrDefault());
                if (checkOwnerEmail == null)
                {
                    var checkCollabEmail = await this.context.Collaborators.SingleOrDefaultAsync(data => data.CollabEmail == collabData.CollabEmail && data.NoteId == collabData.NoteId);
                    if(checkCollabEmail == null)
                    {
                        await this.context.Collaborators.AddAsync(collabData);
                        await this.context.SaveChangesAsync();
                        return collabData;
                    }
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="collabData">The collaborator data.</param>
        /// <returns>
        /// True if collaborator is deleted else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<bool> DeleteCollaborator(int collabId)
        {
            try
            {
                var data = await this.context.Collaborators.SingleOrDefaultAsync(data => data.CollabId == collabId);
                if (data != null)
                {
                    this.context.Collaborators.Remove(data);
                    await this.context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets all collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>
        /// List of all collaborators available on a note
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<CollaboratorsModel>> GetAllCollaborators(int noteId)
        {
            try
            {
                var collaborators = await this.context.Collaborators.Where(data => data.NoteId == noteId).ToListAsync();
                if (collaborators.Count > 0)
                {
                    return collaborators;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
