using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvccore.Migrations.CentralAccess
{
    /// <inheritdoc />
    public partial class InitialMigrationForLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateRegistrationVerified = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegistrationVerifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    DateModified = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmployeeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    DesignationID = table.Column<int>(type: "int", nullable: true),
                    PasswordEncypt = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
