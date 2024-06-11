using Microsoft.EntityFrameworkCore;

using Ap1_P1_AdielGaarciaa.Models;
namespace Ap1_P1_AdielGaarciaa.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options) { }

        public DbSet<Articulos> Articulos {get; set;}
    }
}
