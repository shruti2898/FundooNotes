// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollaboratorsManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FundooModels;

    /// <summary>
    /// Collaborator Manager Interface 
    /// </summary>
    public interface ICollaboratorsManager
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
        Task<bool> DeleteCollaborator(CollaboratorsModel collabData);

        /// <summary>
        /// Gets all collaborators.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>List of all collaborators available on a note</returns>
        Task<IEnumerable<CollaboratorsModel>> GetAllCollaborators(int noteId);
    }
}
