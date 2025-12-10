using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScuolaMVC_RepoStudCorsi.Models
{
    public class Corso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public List<StudenteCorso> Studenti { get; set; } = new();
    }
}
