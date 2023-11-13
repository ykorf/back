public class Contrato
{
    public int Id { get; set; }
    public string CodigoContrato { get; set; }
    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }
    public string Cargo { get; set; }
    public decimal SalarioBase { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
}


public class Funcionario
{
    // Propriedades existentes...

    public List<Contrato> Contratos { get; set; }
}


public class SeuDbContext : DbContext
{
    public SeuDbContext(DbContextOptions<SeuDbContext> options) : base(options)
    {
    }

    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Beneficio> Beneficios { get; set; }
    public DbSet<Frequencia> Frequencias { get; set; }
    public DbSet<Contrato> Contratos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurações adicionais do modelo, como chaves estrangeiras, índices, etc.
        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Funcionario)
            .WithMany(f => f.Contratos)
            .HasForeignKey(c => c.FuncionarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ContratoController : Controller
{
    private readonly SeuDbContext _context;

    public ContratoController(SeuDbContext context)
    {
        _context = context;
    }

    // Ação para exibir detalhes do contrato
    public IActionResult Detalhes(int id)
    {
        var contrato = _context.Contratos
            .Include(c => c.Funcionario)
            .FirstOrDefault(c => c.Id == id);

        if (contrato == null)
        {
            return NotFound();
        }

        return View(contrato);
    }
}
