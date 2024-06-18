namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioBaja(IUsuarioRepositorio repo, IServicioAutorizacion servicio)
{
    public void EjecutarUsuarioBaja (int idUsuario){
        repo.darDeBajaUsuario(idUsuario);
    }
}