// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesModel.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Shruti Sablaniya"/>
// ----------------------------------------------------------------------------------------------------------
namespace FundooModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Notes Model Class
    /// </summary>
    public class NotesModel
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        [Key]
        public int NotesId { get; set; }

        /// <summary>
        /// Gets or sets the note title.
        /// </summary>
        /// <value>
        /// The note title.
        /// </value>
        public string NoteTitle { get; set; }

        /// <summary>
        /// Gets or sets the note description.
        /// </summary>
        /// <value>
        /// The note description.
        /// </value>
        public string NoteDescription { get; set; }

        /// <summary>
        /// Gets or sets the note reminder.
        /// </summary>
        /// <value>
        /// The note reminder.
        /// </value>
        public string NoteReminder { get; set; }

        /// <summary>
        /// Gets or sets the add image.
        /// </summary>
        /// <value>
        /// The add image.
        /// </value>
        public string AddImage { get; set; }

        /// <summary>
        /// Gets or sets the color of the note.
        /// </summary>
        /// <value>
        /// The color of the note.
        /// </value>
        public string NoteColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether /[pin note].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pin note]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool PinNote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [archive note].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [archive note]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool ArchiveNote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [delete note].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [delete note]; otherwise, <c>false</c>.
        /// </value>
        [DefaultValue(false)]
        public bool DeleteNote { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        [ForeignKey("RegisterModel")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the register model.
        /// </summary>
        /// <value>
        /// The register model.
        /// </value>
        public virtual RegisterModel RegisterModel { get; set; }
    }
}
