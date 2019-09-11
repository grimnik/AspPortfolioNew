using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class HomeDetailViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Titel { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string Beschrijving { get; set; }
        public byte[] Foto { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
      
        public List<HomeTagViewModel> Tags { get; set; }
        public List<SelectListItem> Statuses { get; set; }

    }
}
