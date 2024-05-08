using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_Users_FirstUserId",
                table: "ChatUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_Users_SecondUserId",
                table: "ChatUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsTags_Posts_PostId",
                table: "PostsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsTags_Tags_TagId",
                table: "PostsTags");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "GroupChats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsTags",
                table: "PostsTags");

            migrationBuilder.RenameTable(
                name: "PostsTags",
                newName: "PostTags");

            migrationBuilder.RenameColumn(
                name: "SecondUserId",
                table: "ChatUsers",
                newName: "MessageId");

            migrationBuilder.RenameColumn(
                name: "FirstUserId",
                table: "ChatUsers",
                newName: "ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsers_SecondUserId",
                table: "ChatUsers",
                newName: "IX_ChatUsers_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsers_FirstUserId",
                table: "ChatUsers",
                newName: "IX_ChatUsers_ChatId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsTags_TagId",
                table: "PostTags",
                newName: "IX_PostTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostsTags_PostId",
                table: "PostTags",
                newName: "IX_PostTags_PostId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_Chats_ChatId",
                table: "ChatUsers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_Messages_MessageId",
                table: "ChatUsers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_Chats_ChatId",
                table: "ChatUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_Messages_MessageId",
                table: "ChatUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "PostsTags");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "ChatUsers",
                newName: "SecondUserId");

            migrationBuilder.RenameColumn(
                name: "ChatId",
                table: "ChatUsers",
                newName: "FirstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsers_MessageId",
                table: "ChatUsers",
                newName: "IX_ChatUsers_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChatUsers_ChatId",
                table: "ChatUsers",
                newName: "IX_ChatUsers_FirstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagId",
                table: "PostsTags",
                newName: "IX_PostsTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_PostId",
                table: "PostsTags",
                newName: "IX_PostsTags_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsTags",
                table: "PostsTags",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GroupChats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_GroupChats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatId",
                table: "ChatMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_MessageId",
                table: "ChatMessages",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_Users_FirstUserId",
                table: "ChatUsers",
                column: "FirstUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_Users_SecondUserId",
                table: "ChatUsers",
                column: "SecondUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTags_Posts_PostId",
                table: "PostsTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTags_Tags_TagId",
                table: "PostsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
