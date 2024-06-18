using CGE.Aplicacion;
using Microsoft.EntityFrameworkCore;

namespace CGE.Repositorios;

public class CGESqlite{
    public static void Inicializar() {
        using var context = new CGEContext();
        if (context.Database.EnsureCreated()){
            List<Permiso> permisosAdmin = new List<Permiso>();
            permisosAdmin.Add(Permiso.ExpedienteAlta);
            permisosAdmin.Add(Permiso.ExpedienteBaja);
            permisosAdmin.Add(Permiso.ExpedienteModificacion);
            permisosAdmin.Add(Permiso.TramiteAlta);
            permisosAdmin.Add(Permiso.TramiteBaja);
            permisosAdmin.Add(Permiso.TramiteModificacion);
            permisosAdmin.Add(Permiso.PermisoModificacion);
            permisosAdmin.Add(Permiso.UsuarioListado);
            permisosAdmin.Add(Permiso.UsuarioBaja);
            permisosAdmin.Add(Permiso.UsuarioModificacion);
            context.Add(new Usuario(){id=1,nombre="Admin", apellido="", email= "", contraseña= "contraseñaAdmin", permisos=permisosAdmin});
            context.SaveChanges();
        }
        var connection = context.Database.GetDbConnection();
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA journal_mode=DELETE;";
            command.ExecuteNonQuery();
        }
    }
}