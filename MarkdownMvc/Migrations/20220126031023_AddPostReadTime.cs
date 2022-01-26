using Microsoft.EntityFrameworkCore.Migrations;

namespace MarkdownMvc.Migrations
{
    public partial class AddPostReadTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReadTime",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadTime",
                table: "Posts");
        }
    }
}
