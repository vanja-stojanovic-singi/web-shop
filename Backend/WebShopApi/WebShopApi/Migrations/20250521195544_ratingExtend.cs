using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApi.Migrations
{
    /// <inheritdoc />
    public partial class ratingExtend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_OrderItemId",
                table: "Ratings",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_OrderItems_OrderItemId",
                table: "Ratings",
                column: "OrderItemId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_OrderItems_OrderItemId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_OrderItemId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Ratings");
        }
    }
}
