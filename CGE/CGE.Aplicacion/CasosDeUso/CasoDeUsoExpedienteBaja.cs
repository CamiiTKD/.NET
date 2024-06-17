namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteBaja(IExpedienteRepositorio repoExp, ITramiteRepositorio repoTra, IServicioAutorizacion servicio)
{
    public void EjecutarExpedienteBaja(int idExpediente, int idUsuario)
    {
        if(!servicio.PoseeElPermiso(idUsuario, Permiso.ExpedienteBaja)){
            new AutorizacionException($"El usuario con id: {idUsuario} no posee permisos para dar de baja expedientes.");
        }
        Expediente? e = repoExp.consultaPorId(idExpediente);
        if((e.tramites != null)&&(e.tramites.Count != 0)){
            foreach (Tramite t in e.tramites)
            {
                repoTra.darDeBajaTramite(t.Id);
            }
        }
        repoExp.darDeBajaExpediente(idExpediente);
        e = null;
    }
}