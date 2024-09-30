using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mozzafiato.Migrations
{
    /// <inheritdoc />
    public partial class PopularLanches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO Lanches(nome, 
                                                        descricao_curta,
                                                        descricao_detalhada, 
                                                        preco, 
                                                        imagem_url, 
                                                        imagem_thumbnail_url, 
                                                        is_lanche_preferido, 
                                                        em_estoque, 
                                                        categoria_id ) 
                 VALUES ('Pizza de Calabresa', 
                        'Pizza de calabresa com catupiry',
                        'Pizza de calabresa com catupity nas bordas, molho de tomate, orégano e salsinha', 
                        29.99,
                        'https://as2.ftcdn.net/v2/jpg/03/50/38/25/1000_F_350382588_V1E8ehg8rnFuWtaxABYTn3StofEmb6wx.jpg',
                        'https://www.comidaereceitas.com.br/salgados/pizza-de-calabresa.html',
                        True, 
                        True, 
                        1)");

            migrationBuilder.Sql($@"INSERT INTO Lanches(nome, 
                                                        descricao_curta,
                                                        descricao_detalhada, 
                                                        preco, 
                                                        imagem_url, 
                                                        imagem_thumbnail_url, 
                                                        is_lanche_preferido, 
                                                        em_estoque, 
                                                        categoria_id) 
                 VALUES ('Pizza de Chocolate', 
                        'Pizza de chocolate com M&M',
                        'Pizza de Chocolate preto com chocolate branco, M&M, e outros.', 
                        35.99,
                        'https://stock.adobe.com/br/search?k=%22pizza+calabresa%22',
                        'https://www.comidaereceitas.com.br/salgados/pizza-de-calabresa.html',
                        True, 
                        True, 
                        1)");
            migrationBuilder.Sql($@"INSERT INTO Lanches(nome, 
                                                        descricao_curta,
                                                        descricao_detalhada, 
                                                        preco, 
                                                        imagem_url, 
                                                        imagem_thumbnail_url, 
                                                        is_lanche_preferido, 
                                                        em_estoque, 
                                                        categoria_id ) 
                 VALUES ('Pizza de carne de panela e Doritos', 
                        'Pizza de carne de panela desfiada com Doritos, molho de tomate, queijo',
                        'Pizza de carne de panela desfiada com Doritos, molho de tomate, queijo e outros..', 
                        49.99,
                        'https://stock.adobe.com/br/search?k=%22pizza+calabresa%22',
                        'https://www.comidaereceitas.com.br/salgados/pizza-de-calabresa.html',
                        False, 
                        True, 
                        2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
