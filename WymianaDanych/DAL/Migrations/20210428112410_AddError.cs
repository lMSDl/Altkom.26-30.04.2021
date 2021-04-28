using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ErrorId",
                table: "Request",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Error",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Error", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_ErrorId",
                table: "Request",
                column: "ErrorId",
                unique: true,
                filter: "[ErrorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Error_ErrorId",
                table: "Request",
                column: "ErrorId",
                principalTable: "Error",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Request_Error_ErrorId",
                table: "Request");

            migrationBuilder.DropTable(
                name: "Error");

            migrationBuilder.DropIndex(
                name: "IX_Request_ErrorId",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "ErrorId",
                table: "Request");
        }
    }
}
