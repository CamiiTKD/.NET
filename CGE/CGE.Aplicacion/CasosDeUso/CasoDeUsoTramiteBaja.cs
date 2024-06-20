namespace CGE.Aplicacion;
public class CasoDeUsoTramiteBaja(ITramiteRepositorio repoTra, IExpedienteRepositorio repoExp, ServicioActualizacionEstado servicio, IServicioAutorizacion servicioA)
{
    public void EjecutarTramiteBaja(int idTramite, Usuario usuario)
    {
        if (!servicioA.PoseeElPermiso(usuario.permisos, Permiso.TramiteBaja))
        {
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permiso para dar de baja tramites.");
        }
        Tramite? tramite = repoTra.consultaPorTramiteId(idTramite);
        if (tramite != null)
        {
            repoTra.darDeBajaTramite(idTramite);
            Expediente? expediente = repoExp.consultaPorId(tramite.ExpedienteId);
            if (expediente != null)
            {
                servicio.ActualizarEstado(expediente);
                repoExp.ModificarExpediente(expediente, usuario.id);
            }
        }
    }
}