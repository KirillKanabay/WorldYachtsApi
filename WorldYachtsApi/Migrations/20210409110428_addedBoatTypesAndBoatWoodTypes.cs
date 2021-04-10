using Microsoft.EntityFrameworkCore.Migrations;

namespace WorldYachtsApi.Migrations
{
    public partial class addedBoatTypesAndBoatWoodTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "Wood",
                table: "Boats");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Boats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WoodId",
                table: "Boats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BoatTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoatWoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wood = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatWoods", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name", "SecondName" },
                values: new object[] { 1, "Kirill", "Kanabay" });

            migrationBuilder.InsertData(
                table: "BoatTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Шлюпка" },
                    { 2, "Парусная лодка" },
                    { 3, "Галера" }
                });

            migrationBuilder.InsertData(
                table: "BoatWoods",
                columns: new[] { "Id", "Wood" },
                values: new object[,]
                {
                    { 1, "Ель" },
                    { 2, "Береза" },
                    { 3, "Сосна" },
                    { 4, "Дуб" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role", "UserId", "Username" },
                values: new object[] { 1, "admin@gmail.com", "admin", "Admin", 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Boats_TypeId",
                table: "Boats",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_WoodId",
                table: "Boats",
                column: "WoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_BoatTypes_TypeId",
                table: "Boats",
                column: "TypeId",
                principalTable: "BoatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_BoatWoods_WoodId",
                table: "Boats",
                column: "WoodId",
                principalTable: "BoatWoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_BoatTypes_TypeId",
                table: "Boats");

            migrationBuilder.DropForeignKey(
                name: "FK_Boats_BoatWoods_WoodId",
                table: "Boats");

            migrationBuilder.DropTable(
                name: "BoatTypes");

            migrationBuilder.DropTable(
                name: "BoatWoods");

            migrationBuilder.DropIndex(
                name: "IX_Boats_TypeId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_WoodId",
                table: "Boats");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "WoodId",
                table: "Boats");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Boats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Boats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Wood",
                table: "Boats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
