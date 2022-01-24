// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Collaborator Model Class
    /// </summary>
    public class CollaboratorsModel
    {
        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        [Key]
        public int CollabId { get; set; }

        /// <summary>
        /// Gets or sets the collaborator email.
        /// </summary>
        /// <value>
        /// The collaborator email.
        /// </value>
        public string CollabEmail { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }

        /// <summary>
        /// Gets or sets the notes model.
        /// </summary>
        /// <value>
        /// The notes model.
        /// </value>
        public virtual NotesModel NotesModel { get; set; }
    }
}
