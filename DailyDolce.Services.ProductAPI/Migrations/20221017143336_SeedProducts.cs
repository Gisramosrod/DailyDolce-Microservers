using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyDolce.Services.ProductApi.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Cupcake", "Come procuratori e che liberalita che lui sue spezial di impermutabile avvien sempre son tale sua dico prieghi manifesta noi", "https://gisramosrod.blob.core.windows.net/dailydolce/1.jpg", "Pumpkin Cupcake", 15.0 },
                    { 2, "Cookie", "Ma giudice in nostri il carissime seguendo essaudisce per i i ignoranza e giudicio del di che viviamo forse nome", "https://gisramosrod.blob.core.windows.net/dailydolce/2.jpg", "Vanilla Cookies", 15.0 },
                    { 3, "Cake", "Nome piú senza allo degli dalla prieghi i alcun alcun furon sí manifesta di primo cosí potra vostro quale cose", "https://gisramosrod.blob.core.windows.net/dailydolce/3.jpg", "Taiyaki", 15.0 },
                    { 4, "Cake", "Vita mortali suo il da nella accio essaudisce del tanto nome coloro del per e le e quale manifestamente tutte", "https://gisramosrod.blob.core.windows.net/dailydolce/4.jpg", "Strawberry Cake", 15.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
