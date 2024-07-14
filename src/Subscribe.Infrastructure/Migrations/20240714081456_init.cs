using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Subscribe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category_aggregate",
                columns: table => new
                {
                    category_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    icon_file_path = table.Column<string>(type: "text", nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_aggregate", x => x.category_aggregate_id);
                });

            migrationBuilder.CreateTable(
                name: "category_item",
                columns: table => new
                {
                    category_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_name = table.Column<string>(type: "text", nullable: false),
                    category_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_item", x => x.category_item_id);
                    table.ForeignKey(
                        name: "FK_category_item_category_aggregate_category_aggregate_id",
                        column: x => x.category_aggregate_id,
                        principalTable: "category_aggregate",
                        principalColumn: "category_aggregate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscribe_aggregate",
                columns: table => new
                {
                    subscribe_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payment_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expected_date_of_cancellation = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    color_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    is_year = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    category_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    delete_day = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscribe_aggregate", x => x.subscribe_aggregate_id);
                    table.ForeignKey(
                        name: "FK_subscribe_aggregate_category_aggregate_category_aggregate_id",
                        column: x => x.category_aggregate_id,
                        principalTable: "category_aggregate",
                        principalColumn: "category_aggregate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscribe_item",
                columns: table => new
                {
                    subscribe_item_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subscribe_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    subscribe_aggregate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscribe_item", x => x.subscribe_item_id);
                    table.ForeignKey(
                        name: "FK_subscribe_item_subscribe_aggregate_subscribe_aggregate_id",
                        column: x => x.subscribe_aggregate_id,
                        principalTable: "subscribe_aggregate",
                        principalColumn: "subscribe_aggregate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_item_category_aggregate_id",
                table: "category_item",
                column: "category_aggregate_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_item_category_item_id",
                table: "category_item",
                column: "category_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscribe_aggregate_category_aggregate_id",
                table: "subscribe_aggregate",
                column: "category_aggregate_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscribe_item_subscribe_aggregate_id",
                table: "subscribe_item",
                column: "subscribe_aggregate_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscribe_item_subscribe_item_id",
                table: "subscribe_item",
                column: "subscribe_item_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_item");

            migrationBuilder.DropTable(
                name: "subscribe_item");

            migrationBuilder.DropTable(
                name: "subscribe_aggregate");

            migrationBuilder.DropTable(
                name: "category_aggregate");
        }
    }
}
