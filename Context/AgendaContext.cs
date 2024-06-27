using Microsoft.EntityFrameworkCore;
using ProjectMvc.Models;

namespace ProjectMvc.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        public DbSet<Contato> Contatos { get; set; }
    }
}