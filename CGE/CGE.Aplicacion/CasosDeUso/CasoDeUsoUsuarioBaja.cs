namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioBaja(IUsuarioRepositorio repo)
{
    public void EjecutarUsuarioBaja(int idUsuario)
    {
        repo.darDeBajaUsuario(idUsuario);
    }
}