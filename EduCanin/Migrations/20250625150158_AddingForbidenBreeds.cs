using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCanin.Migrations
{
    /// <inheritdoc />
    public partial class AddingForbidenBreeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Breeds_CourseTypes_CourseTypeId",
                table: "Breeds");

            migrationBuilder.DropIndex(
                name: "IX_Breeds_CourseTypeId",
                table: "Breeds");

            migrationBuilder.DropColumn(
                name: "CourseTypeId",
                table: "Breeds");

            migrationBuilder.CreateTable(
                name: "BreedCourseType",
                columns: table => new
                {
                    CourseTypesWithRestrictionId = table.Column<int>(type: "integer", nullable: false),
                    ForbidenBreedId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedCourseType", x => new { x.CourseTypesWithRestrictionId, x.ForbidenBreedId });
                    table.ForeignKey(
                        name: "FK_BreedCourseType_Breeds_ForbidenBreedId",
                        column: x => x.ForbidenBreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BreedCourseType_CourseTypes_CourseTypesWithRestrictionId",
                        column: x => x.CourseTypesWithRestrictionId,
                        principalTable: "CourseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreedCourseType_ForbidenBreedId",
                table: "BreedCourseType",
                column: "ForbidenBreedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreedCourseType");

            migrationBuilder.AddColumn<int>(
                name: "CourseTypeId",
                table: "Breeds",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_CourseTypeId",
                table: "Breeds",
                column: "CourseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Breeds_CourseTypes_CourseTypeId",
                table: "Breeds",
                column: "CourseTypeId",
                principalTable: "CourseTypes",
                principalColumn: "Id");
        }
    }
}
