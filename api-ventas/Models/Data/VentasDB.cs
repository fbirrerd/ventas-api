using api_ventas.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace api_ventas.Models.Data
{
    public class VentasDB : DbContext
    {
        public VentasDB(DbContextOptions<VentasDB> options) : base(options)
        {

        }

        public DbSet<CONSORCIO> Consorcio => Set<CONSORCIO>();
        public DbSet<EMPRESA> Empresa => Set<EMPRESA>();
        public DbSet<UNEGOCIO> UNegocio => Set<UNEGOCIO>();
        public DbSet<LOGIN> Login => Set<LOGIN>();
        public DbSet<PERFIL> Perfil => Set<PERFIL>();

    }
}
