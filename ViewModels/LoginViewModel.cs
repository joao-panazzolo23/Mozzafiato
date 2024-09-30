using System.ComponentModel.DataAnnotations;

namespace Mozzafiato.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Informe o seu usuário")]
        [Display(Name = "Usuário")]
        public string username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Informe a sua senha")]
        [Display(Name = "Senha")]
        public string password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
