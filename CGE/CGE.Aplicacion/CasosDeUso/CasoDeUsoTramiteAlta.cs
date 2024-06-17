namespace CGE.Aplicacion;
public class CasoDeUsoTramiteAlta(ITramiteRepositorio repoTra, IExpedienteRepositorio repoExp, TramiteValidador validador, ServicioActualizacionEstado servicio, IServicioAutorizacion servicioAutorizacion)
{
    public void EjecutarTramiteAlta(Tramite tramite, int idUsuario)
    {
        if(!servicioAutorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteAlta)){
            new AutorizacionException($"El usuario con el id: {idUsuario} no posee permisos para dar de alta trámites.");
        }
        if (validador.Validar(tramite))
        {
            Expediente expediente = repoExp.consultaPorId(tramite.ExpedienteId);
            expediente.AgregarEnLista(tramite);
            repoTra.darDeAltaTramite(tramite);
            EstadoExpediente estadoPrevio = expediente.estado;
            servicio.ActualizarEstado(expediente);
            EstadoExpediente estadoNuevo = expediente.estado;
            if (estadoNuevo != estadoPrevio)
            {
                repoExp.ModificarExpediente(expediente);
            }
        }
    }
}