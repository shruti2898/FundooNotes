// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelRepository.cs" company="Bridgelabz">
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
    /// Label Repository Class
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.ILabelRepository" />
    public class LabelRepository : ILabelRepository
    {
        /// <summary>
        /// The context for Label
        /// </summary>
        private readonly UserContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LabelRepository(UserContext context)
        {
            this.context = context;
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
                var checkLabelNote = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelName == labelData.LabelName && data.NoteId == labelData.NoteId && data.UserId == labelData.UserId);
                if (checkLabelNote == null)
                {  
                    await this.context.Labels.AddAsync(labelData);
                    await this.context.SaveChangesAsync();
                    var userLabel = await this.CreateLabelsForUser(labelData);
                    if (userLabel != null)
                    {
                        userLabel.NoteId = labelData.NoteId;
                        return userLabel;
                    }
                    return labelData;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
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
                var checkUserLabel = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelName == labelData.LabelName && data.NoteId == null && data.UserId == labelData.UserId);
                if (checkUserLabel == null)
                {
                    labelData.NoteId = null;
                    labelData.LabelId = 0;
                    this.context.Add(labelData);
                    this.context.SaveChanges();
                    return labelData;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// List of all labels
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<LabelModel>> GetAllLabels(int userId)
        {
            try
            {
                var labels = await this.context.Labels.Where(data => data.UserId == userId).ToListAsync();
                  if (labels.Count > 0)
                {
                    return labels;
                }

                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets all notes from label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>
        /// List of all notes with same label name
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<IEnumerable<NotesModel>> GetAllNotesFromLabel(int labelId)
        {
            try
            {
                var labelData = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelId == labelId);
                var allNotes = await(from label in this.context.Labels join notes in this.context.Notes on label.NoteId equals notes.NotesId where label.LabelName.Equals(labelData.LabelName) select notes).ToListAsync();
                if (allNotes.Count > 0)
                { 
                    return allNotes;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var labelData = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelId == labelId);
                if (labelData != null)
                {
                    this.context.Labels.Remove(labelData);
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
        /// Deletes the user label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>
        /// True if label is deleted from user else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<bool> DeleteUserLabel(int labelId)
        {
            try
            {
                var labelExist = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelId == labelId);
                if (labelExist !=null)
                {
                    var labels = await this.context.Labels.Where(data => data.UserId == labelExist.UserId && data.LabelName==labelExist.LabelName).ToListAsync();
                    this.context.Labels.RemoveRange(labels);
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
        /// Edits the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>
        /// True if label name is edited successfully else false
        /// </returns>
        /// <exception cref="System.Exception">Throws exception message</exception>
        public async Task<bool> EditLabel(int labelId,LabelModel label)
        {
            try
            {
                var labelData = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelId == labelId  );
                var labels = this.context.Labels.Where(data => data.UserId == label.UserId && data.LabelName.Equals(labelData.LabelName)).ToList();
                if (labels.Count > 0)
                {
                    labels.ForEach(item => item.LabelName = label.LabelName);
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

        public async Task<IEnumerable<LabelModel>> GetNoteLabels(int noteId)
        {
            try
            {
                var labels = await this.context.Labels.Where(data => data.NoteId == noteId).ToListAsync();
                if (labels.Count > 0)
                {
                    return labels;
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
