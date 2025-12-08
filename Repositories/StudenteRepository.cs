
using ScuolaMVC.Data;
using ScuolaMVC.Models;

public class StudenteRepository : Repository<Studente>, IStudenteRepository
{
    public StudenteRepository(ScuolaContext ctx) : base(ctx) {}
}
