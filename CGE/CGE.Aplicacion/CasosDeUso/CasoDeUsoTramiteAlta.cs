namespace CGE.Aplicacion;
public class CasoDeUsoTramiteAlta(ITramiteRepositorio repoTra, IExpedienteRepositorio repoExp, TramiteValidador validador, ServicioActualizacionEstado servicio, IServicioAutorizacion servicioAutorizacion)
{
    public void EjecutarTramiteAlta(Tramite tramite, Usuario usuario)
    {
        if (servicioAutorizacion.PoseeElPermiso(usuario.permisos, Permiso.TramiteAlta))
        {
            if (validador.Validar(tramite))
            {
                Expediente? expediente = repoExp.consultaPorId(tramite.ExpedienteId);
                if (expediente != null)
                {
                    repoTra.darDeAltaTramite(tramite);
                    expediente = servicio.ActualizarEstado(expediente);
                    repoExp.ModificarExpediente(expediente, usuario.id);
                }

            }
        }
        else{
            new AutorizacionException($"El usuario con el id: {usuario.id} no posee permisos para dar de alta trámites.");
        }
    }
}