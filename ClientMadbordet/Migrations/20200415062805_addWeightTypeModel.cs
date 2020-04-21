using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientMadbordet.Migrations
{
    public partial class addWeightTypeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeightType",
                columns: table => new
                {
                    WTID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitName = table.Column<string>(maxLength: 50, nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightType", x => x.WTID);
                });

            migrationBuilder.CreateTable(
                name: "FoodWeightTypes",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false),
                    WeightTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodWeightTypes", x => new { x.FoodId, x.WeightTypeId });
                    table.ForeignKey(
                        name: "FK_FoodWeightTypes_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodWeightTypes_WeightType_WeightTypeId",
                        column: x => x.WeightTypeId,
                        principalTable: "WeightType",
                        principalColumn: "WTID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodWeightTypes_WeightTypeId",
                table: "FoodWeightTypes",
                column: "WeightTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodWeightTypes");

            migrationBuilder.DropTable(
                name: "WeightType");
        }
    }
}
