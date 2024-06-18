namespace CGE.Aplicacion;
public class CasoDeUsoTramiteAlta(ITramiteRepositorio repoTra, IExpedienteRepositorio repoExp, TramiteValidador validador, ServicioActualizacionEstado servicio, IServicioAutorizacion servicioAutorizacion)
{
    public void EjecutarTramiteAlta(Tramite tramite, Usuario usuario)
    {
        if(!servicioAutorizacion.PoseeElPermiso(usuario.permisos, Permiso.TramiteAlta)){
            new AutorizacionException($"El usuario con el id: {usuario.id} no posee permisos para dar de alta trámites.");
            
            if (validador.Validar(tramite))
            {
                Expediente expediente = repoExp.consultaPorId(tramite.ExpedienteId);
                expediente.AgregarEnLista(tramite);
                repoTra.darDeAltaTramite(tramite);
                EstadoExpediente estadoPrevio = expediente.Estado;
                servicio.ActualizarEstado(expediente);
                EstadoExpediente estadoNuevo = expediente.Estado;
                if (estadoNuevo != estadoPrevio)
                {
                    repoExp.ModificarExpediente(expediente);
                }
            }
        }
    }
}