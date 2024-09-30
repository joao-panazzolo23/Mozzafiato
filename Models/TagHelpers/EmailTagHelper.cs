using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net.Mail;
namespace Mozzafiato.Models.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {

        public string Endereco { get; set; }
        public string Conteudo { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "MailAddress:" + Endereco);
            output.Content.SetContent(Conteudo);
        }

    }
}
