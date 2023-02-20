using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_API_Training.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmenttablev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalEmployee = table.Column<int>(type: "int", nullable: false),
                    TotalSalary = table.Column<double>(type: "float", nullable: false),
                    Createddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Createdby = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Updateddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
