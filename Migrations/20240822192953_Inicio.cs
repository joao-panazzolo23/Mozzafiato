using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mozzafiato.Migrations
{
    /// <inheritdoc />
    public partial class Inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    categoria_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    categoria_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorias", x => x.categoria_id);
                });

            migrationBuilder.CreateTable(
                name: "lanches",
                columns: table => new
                {
                    lanche_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    descricao_curta = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    descricao_detalhada = table.Column<string>(type: "text", nullable: true),
                    preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    imagem_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    imagem_thumbnail_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    is_lanche_preferido = table.Column<bool>(type: "boolean", nullable: false),
                    em_estoque = table.Column<bool>(type: "boolean", nullable: false),
                    categoria_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lanches", x => x.lanche_id);
                    table.ForeignKey(
                        name: "fk_lanches_categorias_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "categorias",
                        principalColumn: "categoria_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_lanches_categoria_id",
                table: "lanches",
                column: "categoria_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lanches");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
