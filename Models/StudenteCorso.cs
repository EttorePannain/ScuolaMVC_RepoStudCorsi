using ScuolaMVC_RepoStudCorsi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScuolaMVC_RepoStudCorsi.Models
{
    public class StudenteCorso
    {
        public int StudenteId { get; set; }
        public Studente Studente { get; set; }
        public int CorsoId { get; set; }
        public Corso Corso { get; set; }
    }
}
