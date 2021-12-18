using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepository.Migrations
{
    public partial class Notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NotesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteTitle = table.Column<string>(nullable: true),
                    NoteDescription = table.Column<string>(nullable: true),
                    NoteReminder = table.Column<string>(nullable: true),
                    AddImage = table.Column<string>(nullable: true),
                    NoteColor = table.Column<string>(nullable: true),
                    PinNote = table.Column<bool>(nullable: false),
                    ArchiveNote = table.Column<bool>(nullable: false),
                    DeleteNote = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NotesId);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
