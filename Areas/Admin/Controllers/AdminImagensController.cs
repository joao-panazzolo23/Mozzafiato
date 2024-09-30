using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mozzafiato.Models;

namespace Mozzafiato.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        //essas duas aqui aq sao variaveis de instância
        private readonly ConfigureImagens _myConfig;

        private readonly IWebHostEnvironment _hostingEnvironment;

        //isso aqui é um construtor
        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
            IOptions<ConfigureImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _myConfig = myConfiguration.Value;
        }

        //isso aq é uma action do aspnet, fica dentro do controlador e serve pra ser chamada no action="" da nossa view
        public IActionResult Index()
        {
            return View("Index1");
        }

        //public async task do tipo iaction result é uma tarefa assincrona que realiza uma tarefa em ASPNET
        //O IActionResult é uma interface que representa o resultado da ação do controlador,
        //permitindo retornar diferentes tipos de resultados, como vistas, redirecionamentos, JSON, etc.
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            //se a lista de IFormFiles(Interface do aspnet) files (passada como parâmetro) for nula ou se a contagem dela for zero

            if (files == null || files.Count == 0)

            {
                //executa e retorna a viewdata, uma espécie de objeto do aspnet que pode retornar vários tipos de variaveis
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                //mesma coisa aqui, mas se a quantidade de objs dentro da lista for maior do que 10
                //pra poder usar a viewdata, é necessario importar Microsoft.AspNetCore.Mvc(using)
                ViewData["Erro"] = "Error: Quantidade de arquivos excedeu o limite";
                return View(ViewData);
            }
            //long size é uma variavel do tipo double/long e esse sum é uma lambda (estudar mais sobre dps) que soma
            //os valores de cada um dos arquivos pra saber o total de kb enviados para o servidor
            long size = files.Sum(f => f.Length);
            //cria uma variavel, define ela como uma lista de strings
            var filePathsName = new List<string>();
            //cria uma variavel, define ela como path.combine que serve pra juntar duas strings formatadas para um caminho (sv ou desktop?)
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                   _myConfig.NomePastaImagens);

            //para cada variavel do tipo Iformfile na lista files
            foreach (var formFile in files)
            {   //se o nome do arquivo contem jpg, gif ou png
                if (formFile.FileName.Contains(".jpg")
                    || formFile.FileName.Contains(".gif") ||
                         formFile.FileName.Contains(".png"))
                {
                    //cria uma variavel e define ela como string, concatena as variaveis filepath e filename(de dentro do objeto formFile da classe IFormFile(aspnet)) 
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    //adiciona o caminho à lista de caminhos de arquivo
                    filePathsName.Add(fileNameWithPath);

                    //q beleza pelo menos alguma coisa q eu conheço
                    //using declarado dentro do escopo serve pra dar dispose do obj dps
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        //em cima: define um objeto chamado stream que recebe o tipo FileStream,
                        //que precisa dos argumentos file path e file mode (isso é o que essa função interna vai fazer. nesse caso é criação
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {size} bytes";

            ViewBag.Arquivos = filePathsName;

            //retorna a viewdata
            return View(ViewData);
        }
        //isso é uma função do tipo action (aspnet) que será chamada numa view 
        public IActionResult GetImagens()
        {
            //define uma variavel do tipo filemanagermodel(classe) chamada model que recebe o valor de um novo objeto
            FileManagerModel model = new FileManagerModel();
            //define uma variavel que recebe o valor da combinação dessas strings
            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath,
                 _myConfig.NomePastaImagens);

            //define uma nova variavel do tipo directoryinfo que recebe o directory info com o a string do path das imagens
            DirectoryInfo dir = new DirectoryInfo(userImagesPath);
            FileInfo[] files = dir.GetFiles();
            //define o path das imagens que é uma propriedade do nosso model instanciado lá em cima como NomePastaImagens, 
            //retirado da nossa instancia de configuracao myConfig
            model.PathImagensProdutos = _myConfig.NomePastaImagens;
            //se a quantidade de arquivos for igual a zero, retorna uma viewdata de erro.
            if (files.Length == 0)
            {
                ViewData["Erro"] = $"Nenhum arquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files;
            return View(model);
        }
        // função do tipo Iaction result chamada DeleteFile que tem como parametro uma string chamada filename
        public IActionResult Deletefile(string filename)
        {
            //combina os caminhos da raiz com a string filename 
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaImagens + "\\", filename);
            //se o arquivo existir no sistema
            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                //deleta
                System.IO.File.Delete(_imagemDeleta);
                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }
            return View("index1");
        }
    }
}
