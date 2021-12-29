using FundooModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<NotesModel> Notes { get; set; }
        public DbSet<LabelModel> Labels { get; set; }
        public DbSet<CollaboratorsModel> Collaborators { get; set; }
    }
}
