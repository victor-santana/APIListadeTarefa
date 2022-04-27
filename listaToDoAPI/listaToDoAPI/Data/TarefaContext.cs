using listaToDoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace listaToDoAPI.Data
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> opt) : base(opt)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
