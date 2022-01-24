// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorsRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooRepository.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// Collaborators Repository Interface
    /// </summary>
    public interface ICollaboratorsRepository
    {
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collabData">The collaborator data.</param>
        /// <returns>Collaborator Model Data</returns>
        Task<CollaboratorsModel> AddCollaborator(CollaboratorsModel collabData);

        /// <summary>
        /// Deletes the collaborator.
        /// </summary>
        /// <param name="collabData">The collaborator data.</param>
        /// <returns>True if collaborator is deleted else false</returns>
        Task<bool> DeleteCollaborator(int collabId);

        /// <summary>
        /// Gets all collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>List of all collaborators available on a note</returns>
        Task<IEnumerable<CollaboratorsModel>> GetAllCollaborators(int noteId);
    }
}
