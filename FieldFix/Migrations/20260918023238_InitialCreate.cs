using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FieldFix.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    RequestedBy = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedTo = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ServiceRequests",
                columns: new[] { "Id", "AssignedTo", "Category", "CreatedAt", "Description", "Location", "Priority", "RequestedBy", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Tech A-14", "Infrastructure", new DateTime(2026, 9, 17, 23, 32, 37, 965, DateTimeKind.Utc).AddTicks(3600), "Passenger panel is not responding and requires a field inspection.", "Grand Central", "High", "Transit Operations", "Assigned", "Broken elevator panel" },
                    { 2, "Tech B-09", "Equipment", new DateTime(2026, 9, 18, 0, 32, 37, 965, DateTimeKind.Utc).AddTicks(3610), "Sensor alarm is intermittently triggering on the inbound escalator.", "42nd Street", "Medium", "Station Operations", "In Progress", "Escalator sensor fault" },
                    { 3, "Unassigned", "Safety", new DateTime(2026, 9, 18, 1, 32, 37, 965, DateTimeKind.Utc).AddTicks(3620), "Multiple platform lights are out after a power fluctuation.", "Atlantic Avenue", "Low", "Customer Service", "Open", "Lighting outage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequests");
        }
    }
}
