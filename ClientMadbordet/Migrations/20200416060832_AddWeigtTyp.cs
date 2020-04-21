using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class AddWeigtTyp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodWeightTypes_WeightType_WeightTypeId",
                table: "FoodWeightTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightType",
                table: "WeightType");

            migrationBuilder.RenameTable(
                name: "WeightType",
                newName: "WeightTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightTypes",
                table: "WeightTypes",
                column: "WTID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodWeightTypes_WeightTypes_WeightTypeId",
                table: "FoodWeightTypes",
                column: "WeightTypeId",
                principalTable: "WeightTypes",
                principalColumn: "WTID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodWeightTypes_WeightTypes_WeightTypeId",
                table: "FoodWeightTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightTypes",
                table: "WeightTypes");

            migrationBuilder.RenameTable(
                name: "WeightTypes",
                newName: "WeightType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightType",
                table: "WeightType",
                column: "WTID");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodWeightTypes_WeightType_WeightTypeId",
                table: "FoodWeightTypes",
                column: "WeightTypeId",
                principalTable: "WeightType",
                principalColumn: "WTID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
