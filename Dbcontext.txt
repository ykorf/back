public class SeuDbContext : DbContext
{
    public SeuDbContext(DbContextOptions<SeuDbContext> options) : base(options)
    {
    }

    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Beneficio> Beneficios { get; set; }
    public DbSet<Frequencia> Frequencias { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contrato>()
            .HasOne(c => c.Funcionario)
            .WithMany(f => f.Contratos)
            .HasForeignKey(c => c.FuncionarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
