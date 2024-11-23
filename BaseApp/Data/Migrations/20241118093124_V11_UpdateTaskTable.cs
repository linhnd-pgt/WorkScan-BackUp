using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class V11_UpdateTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "contents",
                table: "task",
                newName: "type");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "parent_task_id",
                table: "task",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "github_repo",
                table: "project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "employee",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date", "password", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 31, 24, 22, DateTimeKind.Local).AddTicks(1910), "$2a$11$qEIbL9pQS4Vvr5y./EhDVOR9cEiZFno6F47UhAb89cyvmBxj5ZerG", new DateTime(2024, 11, 18, 16, 31, 24, 22, DateTimeKind.Local).AddTicks(1938) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 31, 24, 22, DateTimeKind.Local).AddTicks(2460), new DateTime(2024, 11, 18, 16, 31, 24, 22, DateTimeKind.Local).AddTicks(2461) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 31, 24, 22, DateTimeKind.Local).AddTicks(2488), new DateTime(2024, 11, 18, 16, 31, 24, 22, DateTimeKind.Local).AddTicks(2489) });

            migrationBuilder.CreateIndex(
                name: "IX_task_parent_task_id",
                table: "task",
                column: "parent_task_id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_task_parent_task_id",
                table: "task",
                column: "parent_task_id",
                principalTable: "task",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_task_parent_task_id",
                table: "task");

            migrationBuilder.DropIndex(
                name: "IX_task_parent_task_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "description",
                table: "task");

            migrationBuilder.DropColumn(
                name: "parent_task_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "title",
                table: "task");

            migrationBuilder.DropColumn(
                name: "github_repo",
                table: "project");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "task",
                newName: "contents");

            migrationBuilder.AlterColumn<string>(
                name: "note",
                table: "task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "employee",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date", "password", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 15, 10, 11, 5, 519, DateTimeKind.Local).AddTicks(3750), "$2a$11$1xAwTzLktxQopRvZJiH2lenp.RA.ZP/r3m3xB65p4rq7ajn/2PQGq", new DateTime(2024, 11, 15, 10, 11, 5, 519, DateTimeKind.Local).AddTicks(3773) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 15, 10, 11, 5, 519, DateTimeKind.Local).AddTicks(4313), new DateTime(2024, 11, 15, 10, 11, 5, 519, DateTimeKind.Local).AddTicks(4315) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 15, 10, 11, 5, 519, DateTimeKind.Local).AddTicks(4341), new DateTime(2024, 11, 15, 10, 11, 5, 519, DateTimeKind.Local).AddTicks(4341) });
        }
    }
}
