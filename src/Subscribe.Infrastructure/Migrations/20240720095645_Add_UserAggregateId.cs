using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscribe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_UserAggregateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_aggregate_id",
                table: "category_aggregate",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_aggregate_id",
                table: "category_aggregate");
        }
    }
}
