// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooManager.Manager
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;

    /// <summary>
    /// Collaborator Manager class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ICollaboratorsManager" />
    public class CollaboratorsManager : ICollaboratorsManager
    {
        /// <summary>
        /// The collaborator repository
        /// </summary>
        private readonly ICollaboratorsRepository collabRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsManager"/> class.
        /// </summary>
        /// <param name="collabRepository">The collaborator repository.</param>
        public CollaboratorsManager(ICollaboratorsRepository collabRepository)
        {
            this.collabRepository = collabRepository;
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
                return await this.collabRepository.AddCollaborator(collabData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                return await this.collabRepository.DeleteCollaborator(collabId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                return await this.collabRepository.GetAllCollaborators(noteId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
