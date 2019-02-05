using Microsoft.EntityFrameworkCore;

namespace ProCoding.Demos.ASPNETCore.Walkthrough.Models
{
    public class TarefasDbContext : DbContext
    {
        public TarefasDbContext(DbContextOptions<TarefasDbContext> options)
            : base(options)
        { }
        
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}