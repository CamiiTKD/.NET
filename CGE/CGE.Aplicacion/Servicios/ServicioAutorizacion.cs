namespace CGE.Aplicacion;
public class ServicioAutorizacion: IServicioAutorizacion{
    public bool PoseeElPermiso(List<Permiso>permisos, Permiso permiso){
        return permisos.Contains(permiso);
    }
}