namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteBaja(IExpedienteRepositorio repoExp, ITramiteRepositorio repoTra, IServicioAutorizacion servicio)
{
    public void EjecutarExpedienteBaja(int idExpediente, Usuario usuario)
    {
        if (!servicio.PoseeElPermiso(usuario.permisos, Permiso.ExpedienteBaja))
        {
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para dar de baja expedientes.");
        }
        repoExp.darDeBajaExpediente(idExpediente);
    }
}