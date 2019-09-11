using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Domain
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(30)]
        public string Naam { get; set; }
        public ICollection<TagProject> TagProjects { get; set; }
        
    }
}
