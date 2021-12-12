using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace tictactoe.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gameStatuses",
                columns: table => new
                {
                    Status_Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Game_Id = table.Column<int>(nullable: false),
                    Outcome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gameStatuses", x => x.Status_Id);
                });

            migrationBuilder.CreateTable(
                name: "placeTokens",
                columns: table => new
                {
                    token_id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    player_id = table.Column<int>(nullable: false),
                    game_id = table.Column<int>(nullable: false),
                    row = table.Column<int>(nullable: false),
                    col = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_placeTokens", x => x.token_id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    Game_Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    gameStatusStatus_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.Game_Id);
                    table.ForeignKey(
                        name: "FK_games_gameStatuses_gameStatusStatus_Id",
                        column: x => x.gameStatusStatus_Id,
                        principalTable: "gameStatuses",
                        principalColumn: "Status_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "winners",
                columns: table => new
                {
                    Winner_Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Game_Id = table.Column<int>(nullable: false),
                    Player_Id = table.Column<int>(nullable: false),
                    GameStatusStatus_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_winners", x => x.Winner_Id);
                    table.ForeignKey(
                        name: "FK_winners_gameStatuses_GameStatusStatus_Id",
                        column: x => x.GameStatusStatus_Id,
                        principalTable: "gameStatuses",
                        principalColumn: "Status_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    Player_Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Game_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    token_id = table.Column<int>(nullable: true),
                    GameStatusStatus_Id = table.Column<int>(nullable: true),
                    Game_Id1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.Player_Id);
                    table.ForeignKey(
                        name: "FK_players_gameStatuses_GameStatusStatus_Id",
                        column: x => x.GameStatusStatus_Id,
                        principalTable: "gameStatuses",
                        principalColumn: "Status_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_players_games_Game_Id1",
                        column: x => x.Game_Id1,
                        principalTable: "games",
                        principalColumn: "Game_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_players_placeTokens_token_id",
                        column: x => x.token_id,
                        principalTable: "placeTokens",
                        principalColumn: "token_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_gameStatusStatus_Id",
                table: "games",
                column: "gameStatusStatus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_players_GameStatusStatus_Id",
                table: "players",
                column: "GameStatusStatus_Id");

            migrationBuilder.CreateIndex(
                name: "IX_players_Game_Id1",
                table: "players",
                column: "Game_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_players_token_id",
                table: "players",
                column: "token_id");

            migrationBuilder.CreateIndex(
                name: "IX_winners_GameStatusStatus_Id",
                table: "winners",
                column: "GameStatusStatus_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "winners");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "placeTokens");

            migrationBuilder.DropTable(
                name: "gameStatuses");
        }
    }
}
