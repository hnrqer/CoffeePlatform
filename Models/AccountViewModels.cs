using System.ComponentModel.DataAnnotations;
using CompareObsolete = System.Web.Mvc.CompareAttribute;

namespace WebApplication.Models
{

    public class ManageUserViewModel
    {

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [StringLength(20, ErrorMessage = "{0} deve possuir entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [CompareObsolete("NewPassword", ErrorMessage = "Senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }

    public class ManageMailViewModel
    {

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Atual")]
        public string OldEmail { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Novo Email")]
        public string NewEmail { get; set; }

    }

    public class LoginViewModel
    {

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [StringLength(20, ErrorMessage = "{0} deve possuir entre {2} e {1} caracteres.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "{0} está no formato inadequado.")]
        [Display(Name = "Nome de Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        //^[a-zA-Z''-'\s]{1,40}$
        [RegularExpression(@"^[A-Za-z,ã,á,à,â,ê,è,é,í,õ,ú,,ü_ ]+$", ErrorMessage = "{0} está no formato inadequado, somente letras são permitidas.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [StringLength(30, ErrorMessage = "{0} pode possuir no máximo {1} caracteres.")]
        [RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$", ErrorMessage = "Formato do {0} invalido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [StringLength(20, ErrorMessage = "{0} deve possuir entre {2} e {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [CompareObsolete("Password", ErrorMessage = "Senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginSmartphone
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }

    public class ProfileViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int NumProjetos { get; set; }
    }

}










