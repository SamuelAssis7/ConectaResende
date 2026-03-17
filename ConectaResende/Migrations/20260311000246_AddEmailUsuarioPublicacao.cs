using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConectaResende.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailUsuarioPublicacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailUsuario",
                table: "Publicacoes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailUsuario",
                table: "Publicacoes");
        }
    }
}
