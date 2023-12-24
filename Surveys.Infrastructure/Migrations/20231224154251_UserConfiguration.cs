using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surveys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_ApplicationUserProfile_ApplicationUserProfileId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ApplicationUserProfile_ApplicationUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ApplicationUserProfile");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApplicationUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserProfileId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserProfileId",
                table: "AspNetRoles",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_ApplicationUserProfileId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetRoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_ApplicationUserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "AspNetRoles",
                newName: "ApplicationUserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_ApplicationUserId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_ApplicationUserProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserProfileId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserProfile", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserProfileId",
                table: "AspNetUsers",
                column: "ApplicationUserProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_ApplicationUserProfile_ApplicationUserProfileId",
                table: "AspNetRoles",
                column: "ApplicationUserProfileId",
                principalTable: "ApplicationUserProfile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ApplicationUserProfile_ApplicationUserProfileId",
                table: "AspNetUsers",
                column: "ApplicationUserProfileId",
                principalTable: "ApplicationUserProfile",
                principalColumn: "Id");
        }
    }
}
