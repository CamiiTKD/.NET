namespace CGE.Aplicacion;
public interface IUsuarioRepositorio
{
    public void darDeAltaUsuario(Usuario usuario);
    public void darDeBajaUsuario(int idUsuario);
    public void ModificarUsuario(Usuario usuario);
    public List<Usuario> consultaUsuarios();
}