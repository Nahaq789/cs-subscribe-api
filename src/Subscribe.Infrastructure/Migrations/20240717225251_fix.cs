using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscribe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_subscribe_aggregate_category_aggregate_id",
                table: "subscribe_aggregate");

            migrationBuilder.CreateIndex(
                name: "IX_subscribe_aggregate_category_aggregate_id",
                table: "subscribe_aggregate",
                column: "category_aggregate_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_subscribe_aggregate_category_aggregate_id",
                table: "subscribe_aggregate");

            migrationBuilder.CreateIndex(
                name: "IX_subscribe_aggregate_category_aggregate_id",
                table: "subscribe_aggregate",
                column: "category_aggregate_id",
                unique: true);
        }
    }
}
