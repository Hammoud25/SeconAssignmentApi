using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecondAssignmentApi.Migrations
{
    public partial class ExtendedBuyerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Buyers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Buyers",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Buyers");
        }
    }
}
