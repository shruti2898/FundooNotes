// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelManager.cs" company="Bridgelabz">
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
    /// Label Manager Interface
    /// </summary>
    public interface ILabelManager
    {
        /// <summary>
        /// Adds the labels on note.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>Label Model data</returns>
        Task<LabelModel> AddLabelsOnNote(LabelModel labelData);

        /// <summary>
        /// Creates the labels for user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>Label Model data</returns>
        Task<LabelModel> CreateLabelsForUser(LabelModel labelData);

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>List of all labels</returns>
        Task<IEnumerable<LabelModel>> GetAllLabels(int userId);

        Task<IEnumerable<LabelModel>> GetNoteLabels(int noteId);

        /// <summary>
        /// Gets all notes from label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>List of all notes with given label</returns>
        Task<IEnumerable<NotesModel>> GetAllNotesFromLabel(int labelId);

        /// <summary>
        /// Removes the note label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>True if label is removed from note else false</returns>
        Task<bool> RemoveNoteLabel(int labelId);

        /// <summary>
        /// Deletes the user label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>True if label is deleted from user else false</returns>
        Task<bool> DeleteUserLabel(int labelId);

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>True if label name is edited successfully else false</returns>
        Task<bool> EditLabel(int labelId,LabelModel label);
    }
}
