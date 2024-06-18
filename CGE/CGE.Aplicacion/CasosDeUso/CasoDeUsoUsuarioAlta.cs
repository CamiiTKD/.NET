namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioAlta(IUsuarioRepositorio repo)
{
    public void EjecutarAltaUsuario(Usuario usuario)
    {
        repo.darDeAltaUsuario(usuario);
    }
}