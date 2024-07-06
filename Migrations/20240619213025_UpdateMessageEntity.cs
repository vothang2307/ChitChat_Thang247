using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChiChat.Migrations
{
    public partial class UpdateMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "Messages");
        }
    }
}
