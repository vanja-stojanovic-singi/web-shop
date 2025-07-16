﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApi.Migrations
{
    /// <inheritdoc />
    public partial class dbCleanupRl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "longtext",
                nullable: false);
        }
    }
}
