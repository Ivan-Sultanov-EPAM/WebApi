using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Data
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = string.Join("\n", File.ReadAllLines("Data\\SeedDatabaseScript.txt"));
            migrationBuilder.Sql(sql);
        }
    }
}
