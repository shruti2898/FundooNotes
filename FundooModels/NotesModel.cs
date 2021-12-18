using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooModels
{
    public class NotesModel
    {
        [Key]
        public int NotesId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public string NoteReminder { get; set; }
        public string AddImage { get; set; }
        public string NoteColor { get; set; }
        [DefaultValue(false)]
        public bool PinNote { get; set; }
        [DefaultValue(false)]
        public bool ArchiveNote { get; set; }
        [DefaultValue(false)]
        public bool DeleteNote { get; set; }

        [Required]
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
