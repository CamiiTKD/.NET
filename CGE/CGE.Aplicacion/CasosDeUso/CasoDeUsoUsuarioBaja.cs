namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioBaja(IUsuarioRepositorio repo, IServicioAutorizacion servicio)
{
    public void EjecutarUsuarioBaja (int idUsuario, Usuario log){
        if (!servicio.PoseeElPermiso(log.permisos,Permiso.UsuarioBaja)){
            new AutorizacionException($"no posee permisos para realizar bajas de usuarios");
        }
        repo.darDeBajaUsuario(idUsuario);
    }
}