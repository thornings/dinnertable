using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class removeFks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Foods_FoodID_fk",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealID_fk",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_FoodID_fk",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_MealID_fk",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "FoodID_fk",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "MealID_fk",
                table: "FoodItems");

            migrationBuilder.AddColumn<int>(
                name: "FoodID",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MealID",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodID",
                table: "FoodItems",
                column: "FoodID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_MealID",
                table: "FoodItems",
                column: "MealID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Foods_FoodID",
                table: "FoodItems",
                column: "FoodID",
                principalTable: "Foods",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealID",
                table: "FoodItems",
                column: "MealID",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Foods_FoodID",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealID",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_FoodID",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_MealID",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "FoodID",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "MealID",
                table: "FoodItems");

            migrationBuilder.AddColumn<int>(
                name: "FoodID_fk",
                table: "FoodItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealID_fk",
                table: "FoodItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodID_fk",
                table: "FoodItems",
                column: "FoodID_fk");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_MealID_fk",
                table: "FoodItems",
                column: "MealID_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Foods_FoodID_fk",
                table: "FoodItems",
                column: "FoodID_fk",
                principalTable: "Foods",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealID_fk",
                table: "FoodItems",
                column: "MealID_fk",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
