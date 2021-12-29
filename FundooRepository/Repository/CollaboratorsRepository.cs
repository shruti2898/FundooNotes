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
    public class CollaboratorsRepository : ICollaboratorsRepository
    {
        private readonly UserContext context;
        public CollaboratorsRepository(UserContext context)
        {
            this.context = context;
        }
        public async Task<CollaboratorsModel> AddCollaborator(CollaboratorsModel collabData)
        {
            try
            {
                var checkNoteExist =await this.context.Notes.SingleOrDefaultAsync(data => data.NotesId == collabData.NoteId);
                var checkEmailExist = await this.context.Users.SingleOrDefaultAsync(data => data.UserId == checkNoteExist.UserId && data.Email == collabData.CollabEmail);
                var checkCollabExist = await this.context.Collaborators.SingleOrDefaultAsync(data => data.NoteId == collabData.NoteId && data.CollabEmail == collabData.CollabEmail);
               
                if(checkNoteExist != null && checkEmailExist == null && checkCollabExist == null)
                {
                    await this.context.Collaborators.AddAsync(collabData);
                    await this.context.SaveChangesAsync();
                    return collabData;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteCollaborator(CollaboratorsModel collabData)
        {
            try
            {
                var data = await this.context.Collaborators.SingleOrDefaultAsync(data => data.NoteId == collabData.NoteId && data.CollabEmail == collabData.CollabEmail);
                if (data != null)
                {
                    this.context.Collaborators.Remove(data);
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

        public async Task<IEnumerable<CollaboratorsModel>> GetAllCollaborators(int noteId)
        {
            try
            {
                var collaborators = await this.context.Collaborators.Where(data => data.NoteId == noteId).ToListAsync();
                if (collaborators.Count > 0)
                {
                    return collaborators;
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
