﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS2DemoAnalyzer.Migrations
{
    /// <inheritdoc />
    public partial class AddDemoNameToWeapon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DemoName",
                table: "Weapons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemoName",
                table: "Weapons");
        }
    }
}
