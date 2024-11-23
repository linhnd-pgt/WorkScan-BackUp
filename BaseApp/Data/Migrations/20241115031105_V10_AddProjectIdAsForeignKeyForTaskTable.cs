using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class V10_AddProjectIdAsForeignKeyForTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_employee_EmployeeId",
                table: "task");

            migrationBuilder.DropForeignKey(
                name: "FK_task_project_project_id",
                table: "task");

            migrationBuilder.DropIndex(
                name: "IX_task_EmployeeId",
                table: "task");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "task");

            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_task_emp_id",
                table: "task",
                column: "emp_id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_employee_emp_id",
                table: "task",
                column: "emp_id",
                principalTable: "employee",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_task_project_project_id",
                table: "task",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_task_employee_emp_id",
                table: "task");

            migrationBuilder.DropForeignKey(
                name: "FK_task_project_project_id",
                table: "task");

            migrationBuilder.DropIndex(
                name: "IX_task_emp_id",
                table: "task");

            migrationBuilder.DropColumn(
                name: "default_break_end",
                table: "company_info");

            migrationBuilder.DropColumn(
                name: "default_break_start",
                table: "company_info");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "task",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "avatar",
                table: "employee",
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
                values: new object[] { new DateTime(2024, 11, 1, 14, 15, 9, 254, DateTimeKind.Local).AddTicks(5992), "$2a$11$YHzIHhUUoKu12q6Gk93HIOM0DW8CoN6a5HeeFC/HqdcwPM0/ctLky", new DateTime(2024, 11, 1, 14, 15, 9, 254, DateTimeKind.Local).AddTicks(6036) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 1, 14, 15, 9, 254, DateTimeKind.Local).AddTicks(6727), new DateTime(2024, 11, 1, 14, 15, 9, 254, DateTimeKind.Local).AddTicks(6728) });

            migrationBuilder.UpdateData(
                table: "role",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_date", "updated_date" },
                values: new object[] { new DateTime(2024, 11, 1, 14, 15, 9, 254, DateTimeKind.Local).AddTicks(6760), new DateTime(2024, 11, 1, 14, 15, 9, 254, DateTimeKind.Local).AddTicks(6761) });

            migrationBuilder.CreateIndex(
                name: "IX_task_EmployeeId",
                table: "task",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_task_employee_EmployeeId",
                table: "task",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_task_project_project_id",
                table: "task",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
