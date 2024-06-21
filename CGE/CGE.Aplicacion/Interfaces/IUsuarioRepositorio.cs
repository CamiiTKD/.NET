namespace CGE.Aplicacion;
public interface IUsuarioRepositorio
{
    public void darDeAltaUsuario(Usuario usuario);
    public void darDeBajaUsuario(int idUsuario);
    public void ModificarUsuario(Usuario usuario);
    public Usuario? RetornarUsuario(string mail, string contraseña);
    public Usuario? ConsultaUsuario(int id);
    public bool ConsultaUsuarioEmail(string mail);
    public List<Usuario> consultaUsuarios();
}