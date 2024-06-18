namespace CGE.Aplicacion;
public interface IServicioAutorizacion{
    bool PoseeElPermiso(List<Permiso>permisos, Permiso permiso);
}