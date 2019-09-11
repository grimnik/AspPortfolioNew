using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class HomeCreateViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Titel { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string Beschrijving { get; set; }
        public IFormFile Foto { get; set; }
        public List<HomeTagViewModel> Tags { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public string SelectedStatus { get; set; }

    }
}
