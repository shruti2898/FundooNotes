using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly UserContext context;
        public LabelRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task<LabelModel> AddLabels(LabelModel labelData)
        {
            try
            {
                var checkLabelNote = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelName == labelData.LabelName && data.UserId == labelData.UserId && data.NoteId == labelData.NoteId);
                if (checkLabelNote == null)
                {
                    await this.context.Labels.AddAsync(labelData);
                    await this.context.SaveChangesAsync();

                    var checkLabelUser = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelName == labelData.LabelName && data.UserId == labelData.UserId && data.NoteId == null);
                    if (checkLabelUser == null)
                    {
                        labelData.NoteId = null;
                        await this.context.Labels.AddAsync(labelData);
                        await this.context.SaveChangesAsync();
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
        public async Task<List<NotesModel>> GetAllNotesFromLabel(int labelId)
        {
            try
            {
                var labelData = await this.context.Labels.SingleOrDefaultAsync(data => data.LabelId == labelId);
                var allNotes = await (from label in this.context.Labels
                                    join notes in this.context.Notes on label.NoteId equals notes.NotesId
                                    where label.LabelName.Equals(labelData.LabelName)
                                    select notes).ToListAsync();
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
        public async Task<bool> DeleteUserLabel(LabelModel label)
        {
            try
            {
                var labels =  this.context.Labels.Where(data => data.UserId == label.UserId && data.LabelName == label.LabelName).ToList();
                if (labels.Count > 0)
                {
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
        public async Task<bool> EditLabel(LabelModel label)
        {
            try
            {
                var labelData = await this.context.Labels.SingleOrDefaultAsync(data =>data.LabelId == label.LabelId);
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
    }
}
