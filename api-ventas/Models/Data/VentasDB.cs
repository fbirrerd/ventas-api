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

        public DbSet<TConsorcio> Consorcio => Set<TConsorcio>();
        public DbSet<TEmpresa> Empresa => Set<TEmpresa>();
        public DbSet<TUNegocio> UNegocio => Set<TUNegocio>();
        public DbSet<TLogin> Login => Set<TLogin>();
        public DbSet<TPerfil> Perfil => Set<TPerfil>();
        public DbSet<TCatProducto> CatProducto => Set<TCatProducto>();
        public DbSet<TTipoMedida> TipoMedida => Set<TTipoMedida>();
        public DbSet<TProducto> Producto => Set<TProducto>();
        public DbSet<TStock> Stock => Set<TStock>();

    }
}
