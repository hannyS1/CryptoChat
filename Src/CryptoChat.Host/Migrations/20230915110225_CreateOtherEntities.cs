using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoChat.Host.Migrations
{
    /// <inheritdoc />
    public partial class CreateOtherEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    text = table.Column<string>(type: "TEXT", nullable: true),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    room_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.id);
                    table.ForeignKey(
                        name: "FK_message_room_room_id",
                        column: x => x.room_id,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_message_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_room",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false),
                    room_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_room", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_room_room_room_id",
                        column: x => x.room_id,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_room_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_name",
                table: "user",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_message_room_id",
                table: "message",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_user_id",
                table: "message",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_room_room_id",
                table: "user_room",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_room_user_id",
                table: "user_room",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "user_room");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropIndex(
                name: "IX_user_name",
                table: "user");
        }
    }
}
