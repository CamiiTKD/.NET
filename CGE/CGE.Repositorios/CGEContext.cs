using CGE.Aplicacion;
using Microsoft.EntityFrameworkCore;

namespace CGE.Repositorios;

public class CGEContext : DbContext{
    public DbSet<Usuario> Usuarios{ get; set;}
    public DbSet<Expediente> Expedientes { get; set;}
    public DbSet<Tramite> Tramites { get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=..\\CGE.Repositorios\\CGEDataBase.sqlite");
    }
}