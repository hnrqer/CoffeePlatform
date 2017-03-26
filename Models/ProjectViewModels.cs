using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [StringLength(20, ErrorMessage = "{0} deve possuir entre {2} e {1} caracteres.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "{0} está no formato inadequado.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "{0} pode possuir no máximo {1} caracteres.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        public string userID { get; set; }
    }

    public class ProjectDataViewModel
    {
        public int ID { get; set; }
        public string BlocklyData { get; set; }
        public string BlocklyCode { get; set; }
    }

    public class ProjectConfigViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Por favor, preencha o campo {0}.")]
        [StringLength(20, ErrorMessage = "{0} deve possuir entre {2} e {1} caracteres.", MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "{0} está no formato inadequado.")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "{0} pode possuir no máximo {1} caracteres.")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [Display(Name = "Data de Criação")]
        public DateTime Date { get; set; }
        [Display(Name = "Compartilhar com")]
        public string ShareTo { get; set; }
        [Display(Name = "Compartilhado por")]
        public string SharedBy { get; set; }
        [Display(Name = "Público")]
        public bool Public { get; set; }
    }

    public class ProjectListViewModel
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
     public class ProjectCheckedListViewModel
    {
        public int ProjectId { get; set; }
        public bool Checked { get; set; }

    }

     public class ProjectPublicView
     {
         public int ProjectID { get; set; }
         public string ProjectName { get; set; }
         public string ProjectDescription { get; set; }
         public string UserName { get; set; }
     }

}