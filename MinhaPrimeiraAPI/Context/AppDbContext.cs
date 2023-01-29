using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraAPI.Models;

namespace MinhaPrimeiraAPI.Context
{
    // "Context" Responsável pela interação com os objetos e administra os objetos durante o tempo de execução.
    public class AppDbContext : DbContext // "DbContext" inicializa uma nova instância da classe.
    {
        // Representa uma sessão com o banco de dados para executar operações do CRUD.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        // Mapeia a entidade
        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("AlteradoEm").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

// Migration

// add - migration Inicial - Context AppDbContext - StartupProject MinhaPrimeiraAPI - Project MinhaPrimeiraAPI

// update - database - Context AppDbContext - StartupProject MinhaPrimeiraAPI - Project MinhaPrimeiraAPI