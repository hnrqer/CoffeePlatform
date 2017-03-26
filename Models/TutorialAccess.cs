using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.ComponentModel;

namespace WebApplication.Models
{
    public class TutorialAccess
    {
        public int id { get; set; }
        [DefaultValue(0)]
        public int counter { get; set; }

    }

}