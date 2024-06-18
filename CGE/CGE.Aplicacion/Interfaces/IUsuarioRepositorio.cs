namespace CGE.Aplicacion;
public interface IUsuarioRepositorio
{
    public Usuario consultaUsuarioId(int idUsuario);
    public void darDeAltaUsuario(Usuario usuario);
    public void darDeBajaUsuario(int idUsuario);
    public void ModificarUsuario(Usuario usuario);
}