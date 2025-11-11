using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgyptCitiesApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "CommonSequence",
                schema: "dbo",
                startValue: 100L,
                incrementBy: 5);

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.CommonSequence"),
                    governorate_name_ar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    governorate_name_en = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.CommonSequence"),
                    governorate_id = table.Column<int>(type: "int", nullable: false),
                    city_name_ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    city_name_en = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Governorates_governorate_id",
                        column: x => x.governorate_id,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_governorate_id",
                table: "Cities",
                column: "governorate_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Governorates");

            migrationBuilder.DropSequence(
                name: "CommonSequence",
                schema: "dbo");
        }
    }
}
