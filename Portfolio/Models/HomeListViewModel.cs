using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class HomeListViewModel 
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }
        public byte[] Foto { get; set; }
        public Status Status { get; set; }
        public ICollection<TagProject> TagProjects { get; set; }
    }
}
