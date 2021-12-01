using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeBookApi.Migrations
{
    public partial class renametables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeBook_RecipeBookId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeBookId",
                table: "Recipe",
                newName: "IX_Recipe_RecipeBookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_RecipeBook_RecipeBookId",
                table: "Recipe",
                column: "RecipeBookId",
                principalTable: "RecipeBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_RecipeBook_RecipeBookId",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameIndex(
                name: "IX_Recipe_RecipeBookId",
                table: "Recipes",
                newName: "IX_Recipes_RecipeBookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeBook_RecipeBookId",
                table: "Recipes",
                column: "RecipeBookId",
                principalTable: "RecipeBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
