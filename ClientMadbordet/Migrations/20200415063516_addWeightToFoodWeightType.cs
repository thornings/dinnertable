using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class addWeightToFoodWeightType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "WeightType");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "FoodWeightTypes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "FoodWeightTypes");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "WeightType",
                nullable: false,
                defaultValue: 0);
        }
    }
}
