//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

//namespace Inventory_Management.Migrations
//{
//    /// <inheritdoc />
//    public partial class AddCategoryTable : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.RenameColumn(
//                name: "Category",
//                table: "Products",
//                newName: "CategoryID");

//            migrationBuilder.CreateTable(
//                name: "Categories",
//                columns: table => new
//                {
//                    ID = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Categories", x => x.ID);
//                });

//            migrationBuilder.InsertData(
//                table: "Categories",
//                columns: new[] { "ID", "IsDeleted", "Name" },
//                values: new object[,]
//                {
//                    { 1, false, "Snacks & Sweets" },
//                    { 2, false, "Devices" },
//                    { 3, false, "Grocery" },
//                    { 4, false, "Clothes" },
//                    { 5, false, "Others" }
//                });

//            migrationBuilder.InsertData(
//                table: "Products",
//                columns: new[] { "ID", "Available", "CategoryID", "CreatedAt", "ExpiryDate", "ImageUrl", "IsDeleted", "Name", "Price", "Quantity", "Threshold", "Unit" },
//                values: new object[,]
//                {
//                    { 1, 0, 3, new DateTime(2024, 11, 6, 15, 16, 15, 926, DateTimeKind.Utc).AddTicks(6477), new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730568418/maggi-noodles-1000x1000.jpg", false, "Maggi", 12m, 50, 22, "g1" },
//                    { 2, 0, 1, new DateTime(2024, 11, 6, 15, 16, 15, 926, DateTimeKind.Utc).AddTicks(6482), new DateTime(2025, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730568627/6223000508572_-_37g.webp", false, "Tiger", 7m, 40, 20, "g1" },
//                    { 3, 2, 5, new DateTime(2024, 11, 6, 15, 16, 15, 926, DateTimeKind.Utc).AddTicks(6485), new DateTime(2026, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730656198/caregory.jpg", false, "dexdece", 250m, 7, 5, "g1" },
//                    { 4, 1, 5, new DateTime(2024, 11, 6, 15, 16, 15, 926, DateTimeKind.Utc).AddTicks(6488), new DateTime(2025, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "http://res.cloudinary.com/dpapfkrx1/image/upload/v1730658274/01d4fdc0e786083bd7002de356fb29c3.jpg", false, "product2", 100m, 12, 22, "g1" }
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_Products_CategoryID",
//                table: "Products",
//                column: "CategoryID");

//            migrationBuilder.AddForeignKey(
//                name: "FK_Products_Categories_CategoryID",
//                table: "Products",
//                column: "CategoryID",
//                principalTable: "Categories",
//                principalColumn: "ID",
//                onDelete: ReferentialAction.Cascade);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropForeignKey(
//                name: "FK_Products_Categories_CategoryID",
//                table: "Products");

//            migrationBuilder.DropTable(
//                name: "Categories");

//            migrationBuilder.DropIndex(
//                name: "IX_Products_CategoryID",
//                table: "Products");

//            migrationBuilder.DeleteData(
//                table: "Products",
//                keyColumn: "ID",
//                keyValue: 1);

//            migrationBuilder.DeleteData(
//                table: "Products",
//                keyColumn: "ID",
//                keyValue: 2);

//            migrationBuilder.DeleteData(
//                table: "Products",
//                keyColumn: "ID",
//                keyValue: 3);

//            migrationBuilder.DeleteData(
//                table: "Products",
//                keyColumn: "ID",
//                keyValue: 4);

//            migrationBuilder.RenameColumn(
//                name: "CategoryID",
//                table: "Products",
//                newName: "Category");
//        }
//    }
//}
