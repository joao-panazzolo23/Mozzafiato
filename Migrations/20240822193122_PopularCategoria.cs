using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mozzafiato.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Insert into Categorias(Categoria_Name, Descricao) values ('Normal','lanche feito com ingredientes Normais') ");
            migrationBuilder.Sql($@"Insert into Categorias(Categoria_Name, Descricao) values ('Natural','lanche feito com ingredientes integrais') ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
