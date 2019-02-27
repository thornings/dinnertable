using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class changefoodid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Foods_FoodRef",
                table: "FoodItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealRef",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_FoodRef",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_MealRef",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "FoodRef",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "MealRef",
                table: "FoodItems");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "FoodRef",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MealRef",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_FoodRef",
                table: "FoodItems",
                column: "FoodRef");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_MealRef",
                table: "FoodItems",
                column: "MealRef");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Foods_FoodRef",
                table: "FoodItems",
                column: "FoodRef",
                principalTable: "Foods",
                principalColumn: "FoodID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealRef",
                table: "FoodItems",
                column: "MealRef",
                principalTable: "Meals",
                principalColumn: "MealID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
