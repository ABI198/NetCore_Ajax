using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Ajax.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
