
using ScuolaMVC_RepoStudCorsi.Data;
using ScuolaMVC_RepoStudCorsi.Models;

public class StudenteRepository : Repository<Studente>, IStudenteRepository
{
    public StudenteRepository(ScuolaContext ctx) : base(ctx) {}
}
