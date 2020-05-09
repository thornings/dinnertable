using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class AddFoodWeightTypeToCalendarFoodItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedFoodWeightTypeFoodId",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedFoodWeightTypeWeightTypeId",
                table: "FoodItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_SelectedFoodWeightTypeFoodId_SelectedFoodWeightTypeWeightTypeId",
                table: "FoodItems",
                columns: new[] { "SelectedFoodWeightTypeFoodId", "SelectedFoodWeightTypeWeightTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_FoodWeightTypes_SelectedFoodWeightTypeFoodId_SelectedFoodWeightTypeWeightTypeId",
                table: "FoodItems",
                columns: new[] { "SelectedFoodWeightTypeFoodId", "SelectedFoodWeightTypeWeightTypeId" },
                principalTable: "FoodWeightTypes",
                principalColumns: new[] { "FoodId", "WeightTypeId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_FoodWeightTypes_SelectedFoodWeightTypeFoodId_SelectedFoodWeightTypeWeightTypeId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_SelectedFoodWeightTypeFoodId_SelectedFoodWeightTypeWeightTypeId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "SelectedFoodWeightTypeFoodId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "SelectedFoodWeightTypeWeightTypeId",
                table: "FoodItems");
        }
    }
}
