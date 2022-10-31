using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Domain.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleGroups_RoleGroupId1",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleGroupId1",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleGroupId1",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "RoleGroupId",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleGroupId",
                table: "Roles",
                column: "RoleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_RoleGroups_RoleGroupId",
                table: "Roles",
                column: "RoleGroupId",
                principalTable: "RoleGroups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_RoleGroups_RoleGroupId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleGroupId",
                table: "Roles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleGroupId",
                table: "Roles",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleGroupId1",
                table: "Roles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleGroupId1",
                table: "Roles",
                column: "RoleGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_RoleGroups_RoleGroupId1",
                table: "Roles",
                column: "RoleGroupId1",
                principalTable: "RoleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
