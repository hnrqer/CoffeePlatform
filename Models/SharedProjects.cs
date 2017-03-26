using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Models
{
    public class SharedProject
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [Key]
        [Column(Order = 1)]
        public string SharedToId { get; set; }
        [ForeignKey("SharedToId")]
        public virtual ApplicationUser UserSharedTo { get; set; }

        //Nao considerei foreignKey para sharedBy porque gera um erro constraint, no qual proibe utilizar OndDeleCascade para
        //o a tabela Users. Portanto, n vejo problema deixar armazenado o SharedByUser caso o usuario que compartilhou o projeto 
        //deletar sua propria conta.
        public string SharedById { get; set; }
        
        public bool Verified { get; set; }
    }
}