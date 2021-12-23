using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class CollaboratorsModel
    {
        [Key]
        public int CollaboratorId { get; set; }
        public string CollaboratorsEmail { get; set; }
        public string UserEmail { get; set; }

        [ForeignKey("NotesModel")]
        public int NotesId { get; set; }
        public virtual NotesModel NotesModel { get; set; }
    }
}
