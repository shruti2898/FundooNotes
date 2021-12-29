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
        public int CollabId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("NotesModel")]
        public int NoteId { get; set; }
        public virtual NotesModel NotesModel { get; set; }
    }
}
