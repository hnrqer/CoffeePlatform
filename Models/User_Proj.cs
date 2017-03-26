using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WebApplication.Models
{
    public class User_Proj
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        
        public virtual Project Project { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        
        public virtual ApplicationUser User { get; set; }
    }

}