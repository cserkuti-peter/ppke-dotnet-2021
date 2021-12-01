using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBookApi.Migrations
{
    public partial class RecipeBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeBookId",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RecipeBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeBook", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RecipeBook",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "...", "My favorite recipes" });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecipeBookId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecipeBookId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_RecipeBookId",
                table: "Recipes",
                column: "RecipeBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeBook_RecipeBookId",
                table: "Recipes",
                column: "RecipeBookId",
                principalTable: "RecipeBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeBook_RecipeBookId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeBook");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_RecipeBookId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeBookId",
                table: "Recipes");
        }
    }
}
