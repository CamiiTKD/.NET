namespace CGE.Aplicacion;

public class ServicioSesion : IServicioSesion
{
    public Usuario? UsuarioLogged { get; set; }
    public bool Conectado { get; set; }
    public void logOut()
    {
        UsuarioLogged = null;
        Conectado = false;
    }
}
