namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteBaja(IExpedienteRepositorio repoExp, ITramiteRepositorio repoTra, IServicioAutorizacion servicio)
{
    public void EjecutarExpedienteBaja(int idExpediente, Usuario usuario)
    {
        if(!servicio.PoseeElPermiso(usuario.permisos, Permiso.ExpedienteBaja)){
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para dar de baja expedientes.");
        }
        Expediente? e = repoExp.consultaPorId(idExpediente);
        if((e.Tramites != null)&&(e.Tramites.Count != 0)){
            foreach (Tramite t in e.Tramites)
            {
                repoTra.darDeBajaTramite(t.Id);
            }
        }
        repoExp.darDeBajaExpediente(idExpediente);
        e = null;
    }
}