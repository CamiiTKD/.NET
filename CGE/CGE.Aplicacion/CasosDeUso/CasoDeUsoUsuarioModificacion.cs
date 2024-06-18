namespace CGE.Aplicacion;
public class CasoDeUsoUsuarioModificacion(IUsuarioRepositorio repo, IServicioAutorizacion servicio)
{
    public void EjecutarModificarUsuario (Usuario usuario/*,Usuario log*/){
        /*if (!servicio.PoseeElPermiso(log.permisos,Permiso.UsuarioModificacion)){
            new AutorizacionException($"no posee permisos para realizar modificaciones");
        }*/
        repo.ModificarUsuario(usuario);
    }
}