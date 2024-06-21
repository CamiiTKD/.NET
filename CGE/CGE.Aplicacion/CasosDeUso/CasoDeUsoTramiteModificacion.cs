namespace CGE.Aplicacion;
public class CasoDeUsoTramiteModificacion(ITramiteRepositorio repoTra, TramiteValidador validador, IExpedienteRepositorio repoExp, ServicioActualizacionEstado servicio, IServicioAutorizacion servicioAutorizacion)
{
    public void EjecutarModificacionTramite(Tramite tramite, Expediente expe, Usuario usuario)
    {
        if(!servicioAutorizacion.PoseeElPermiso(usuario.permisos, Permiso.TramiteModificacion)){
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para modificar trámites.");
        }
        if (validador.Validar(tramite))
        {
            tramite.ModificarUltimaFecha(usuario.id);
            repoTra.ModificarTramite(tramite, usuario.id);
            EstadoExpediente estado = expe.Estado;
            expe = servicio.ActualizarEstado(expe);
            if (expe.Estado != estado){
                expe.ModificarUltimaFecha(usuario.id);
            }
            repoExp.ModificarExpediente(expe, usuario.id);
        }
        else {
            new ValidacionException($"El Tramite{tramite.Id} no fue validado"); 
        }
    }
}