using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        Task<LabelModel> AddLabels(LabelModel labelData);
        Task<IEnumerable<LabelModel>> GetAllLabels(int userId);
        Task<List<NotesModel>> GetAllNotesFromLabel(int labelId);
        Task<bool> RemoveNoteLabel(int labelId);
        Task<bool> DeleteUserLabel(LabelModel label);
        Task<bool> EditLabel(LabelModel label);
    }
}
