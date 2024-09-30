using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mozzafiato.Migrations
{
    /// <inheritdoc />
    public partial class Pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    pedido_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    endereco2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cep = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    telefone = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    email = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    pedido_total = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    total_itens_pedido = table.Column<int>(type: "integer", nullable: false),
                    pedido_enviado = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pedido_entregue_em = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedidos", x => x.pedido_id);
                });

            migrationBuilder.CreateTable(
                name: "pedido_detalhes",
                columns: table => new
                {
                    pedido_detalhe_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pedido_id = table.Column<int>(type: "integer", nullable: false),
                    lanche_id = table.Column<int>(type: "integer", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    preco = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedido_detalhes", x => x.pedido_detalhe_id);
                    table.ForeignKey(
                        name: "fk_pedido_detalhes_lanches_lanche_id",
                        column: x => x.lanche_id,
                        principalTable: "lanches",
                        principalColumn: "lanche_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pedido_detalhes_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "pedido_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_pedido_detalhes_lanche_id",
                table: "pedido_detalhes",
                column: "lanche_id");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_detalhes_pedido_id",
                table: "pedido_detalhes",
                column: "pedido_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido_detalhes");

            migrationBuilder.DropTable(
                name: "pedidos");
        }
    }
}
