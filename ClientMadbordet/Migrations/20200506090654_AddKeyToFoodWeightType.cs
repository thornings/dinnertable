using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class AddKeyToFoodWeightType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodWeightTypeID",
                table: "FoodWeightTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_FoodWeightTypes_FoodWeightTypeID",
                table: "FoodWeightTypes",
                column: "FoodWeightTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_FoodWeightTypes_FoodWeightTypeID",
                table: "FoodWeightTypes");

            migrationBuilder.DropColumn(
                name: "FoodWeightTypeID",
                table: "FoodWeightTypes");
        }
    }
}
