using CGE.Aplicacion;
using Microsoft.EntityFrameworkCore;

namespace CGE.Repositorios;

public class CGESqlite{
    public static void Inicializar() {
        using var context = new CGEContext();
        context.Database.EnsureCreated();
        
        var connection = context.Database.GetDbConnection();
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA journal_mode=DELETE;";
            command.ExecuteNonQuery();
        }
    }
}