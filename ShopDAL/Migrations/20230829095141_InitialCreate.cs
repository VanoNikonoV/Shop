using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id_customer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    middle_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    telefon = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    e_mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id_customer);
                    table.UniqueConstraint("AK_customers_e_mail", x => x.e_mail);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    e_mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    product_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    name_product = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id_product);
                    table.ForeignKey(
                        name: "FK_orders_customers_e_mail",
                        column: x => x.e_mail,
                        principalTable: "customers",
                        principalColumn: "e_mail",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_e_mail",
                table: "customers",
                column: "e_mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_e_mail",
                table: "orders",
                column: "e_mail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "customers");
        }
    }
}
