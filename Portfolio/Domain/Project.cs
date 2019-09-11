using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Domain
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Titel { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string Beschrijving { get; set; }
        public byte[] Foto { get; set; }
        [Required]
        public int StatusId { get; set; }
       
        public Status Status { get; set; }
        public ICollection<TagProject> TagProjects { get; set; }
    }
}
    