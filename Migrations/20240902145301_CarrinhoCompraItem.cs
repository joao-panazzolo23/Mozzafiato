using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mozzafiato.Migrations
{
    /// <inheritdoc />
    public partial class CarrinhoCompraItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarrinhoCompraItens",
                columns: table => new
                {
                    carrinho_compra_item_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lanche_id = table.Column<int>(type: "integer", nullable: true),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    carrinho_compra_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_carrinho_compra_itens", x => x.carrinho_compra_item_id);
                    table.ForeignKey(
                        name: "fk_carrinho_compra_itens_lanches_lanche_id",
                        column: x => x.lanche_id,
                        principalTable: "lanches",
                        principalColumn: "lanche_id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_carrinho_compra_itens_lanche_id",
                table: "CarrinhoCompraItens",
                column: "lanche_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompraItens");
        }
    }
}
