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
    public class Project
    {
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastSaved { get; set; }
        
        public string BlocklyData { get; set; }
        public string BlocklyCode { get; set; }
        public bool Public { get; set; }

    }
}