// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelManager.cs" company="Bridgelabz">
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
    /// Label Manager Class
    /// </summary>
    /// <seealso cref="FundooManager.Interface.ILabelManager" />
    public class LabelManager : ILabelManager
    {
        /// <summary>
        /// The label repository
        /// </summary>
        private readonly ILabelRepository labelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelManager"/> class.
        /// </summary>
        /// <param name="labelRepository">The label repository.</param>
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        /// <summary>
        /// Adds the labels.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// Label Model data
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<LabelModel> AddLabelsOnNote(LabelModel labelData)
        {
            try
            {
                return await this.labelRepository.AddLabelsOnNote(labelData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Creates the labels for user.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>
        /// Label Model data
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<LabelModel> CreateLabelsForUser(LabelModel labelData)
        {
            try
            {
                return await this.labelRepository.CreateLabelsForUser(labelData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletes the user label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>True if label is deleted from user else false</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<bool> DeleteUserLabel(LabelModel labelData)
        {
            try
            {
                return await this.labelRepository.DeleteUserLabel(labelData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Edits the label.
        /// </summary>
        /// <param name="labelData">The label data.</param>
        /// <returns>True if label name is edited successfully else false</returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<bool> EditLabel(LabelModel labelData)
        {
            try
            {
                return await this.labelRepository.EditLabel(labelData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of all labels created by user
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<LabelModel>> GetAllLabels(int userId)
        {
            try
            {
                return await this.labelRepository.GetAllLabels(userId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Gets all notes from label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// List of all notes with given label
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<List<NotesModel>> GetAllNotesFromLabel(int labelId)
        {
            try
            {
                return await this.labelRepository.GetAllNotesFromLabel(labelId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Removes the note label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// True if label is removed from note else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<bool> RemoveNoteLabel(int labelId)
        {
            try
            {
                return await this.labelRepository.RemoveNoteLabel(labelId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
