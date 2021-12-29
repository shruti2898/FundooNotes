using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository labelRepository;
        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }

        public async Task<LabelModel> AddLabels(LabelModel labelData)
        {
            try
            {
                return await this.labelRepository.AddLabels(labelData);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

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
