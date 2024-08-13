using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Desafio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Nat = table.Column<string>(type: "text", nullable: false),
                    ContactInformation_Cell = table.Column<string>(type: "text", nullable: false),
                    ContactInformation_Email = table.Column<string>(type: "text", nullable: false),
                    ContactInformation_Phone = table.Column<string>(type: "text", nullable: false),
                    DateOfBirthInformation_Age = table.Column<int>(type: "integer", nullable: false),
                    DateOfBirthInformation_DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    DocumentInformation_Number = table.Column<string>(type: "text", nullable: false),
                    DocumentInformation_Type = table.Column<string>(type: "text", nullable: false),
                    ImageInformation_Thumbnail = table.Column<string>(type: "text", nullable: false),
                    LocationInformation_City = table.Column<string>(type: "text", nullable: false),
                    LocationInformation_Country = table.Column<string>(type: "text", nullable: false),
                    LocationInformation_Name = table.Column<string>(type: "text", nullable: false),
                    LocationInformation_Number = table.Column<int>(type: "integer", nullable: false),
                    LocationInformation_Postcode = table.Column<int>(type: "integer", nullable: false),
                    LocationInformation_State = table.Column<string>(type: "text", nullable: false),
                    LoginInformation_Password = table.Column<string>(type: "text", nullable: false),
                    LoginInformation_SHA256 = table.Column<string>(type: "text", nullable: false),
                    LoginInformation_Salt = table.Column<string>(type: "text", nullable: false),
                    LoginInformation_Username = table.Column<string>(type: "text", nullable: false),
                    NameInformation_First = table.Column<string>(type: "text", nullable: false),
                    NameInformation_Last = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
