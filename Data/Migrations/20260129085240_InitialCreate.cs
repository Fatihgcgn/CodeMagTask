using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GLN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GTIN = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerialStart = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LogisticUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SSCC = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogisticUnits_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Serials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GTIN = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gs1String = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serials_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AggregationLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentLogisticUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChildType = table.Column<int>(type: "int", nullable: false),
                    ChildLogisticUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChildSerialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregationLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AggregationLinks_LogisticUnits_ChildLogisticUnitId",
                        column: x => x.ChildLogisticUnitId,
                        principalTable: "LogisticUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AggregationLinks_LogisticUnits_ParentLogisticUnitId",
                        column: x => x.ParentLogisticUnitId,
                        principalTable: "LogisticUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AggregationLinks_Serials_ChildSerialId",
                        column: x => x.ChildSerialId,
                        principalTable: "Serials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AggregationLinks_ChildLogisticUnitId",
                table: "AggregationLinks",
                column: "ChildLogisticUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationLinks_ChildSerialId",
                table: "AggregationLinks",
                column: "ChildSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationLinks_ParentLogisticUnitId",
                table: "AggregationLinks",
                column: "ParentLogisticUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationLinks_ParentLogisticUnitId_ChildLogisticUnitId",
                table: "AggregationLinks",
                columns: new[] { "ParentLogisticUnitId", "ChildLogisticUnitId" },
                unique: true,
                filter: "[ChildLogisticUnitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AggregationLinks_ParentLogisticUnitId_ChildSerialId",
                table: "AggregationLinks",
                columns: new[] { "ParentLogisticUnitId", "ChildSerialId" },
                unique: true,
                filter: "[ChildSerialId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_GLN",
                table: "Customers",
                column: "GLN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogisticUnits_SSCC",
                table: "LogisticUnits",
                column: "SSCC",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogisticUnits_WorkOrderId",
                table: "LogisticUnits",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId_GTIN",
                table: "Products",
                columns: new[] { "CustomerId", "GTIN" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Serials_GTIN_SerialNo",
                table: "Serials",
                columns: new[] { "GTIN", "SerialNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Serials_WorkOrderId",
                table: "Serials",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ProductId",
                table: "WorkOrders",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregationLinks");

            migrationBuilder.DropTable(
                name: "LogisticUnits");

            migrationBuilder.DropTable(
                name: "Serials");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
