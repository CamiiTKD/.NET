namespace CGE.Aplicacion;
public class ServicioAutorizacion : IServicioAutorizacion
{
    public bool PoseeElPermiso(List<Permiso>? permisos, Permiso permiso)
    {
        if (permisos != null)
        {
            return permisos.Contains(permiso);
        }
        else
        {
            return false;
        }
    }
}