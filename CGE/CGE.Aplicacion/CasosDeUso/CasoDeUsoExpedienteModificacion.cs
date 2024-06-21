namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteModificacion(IExpedienteRepositorio repoExp, ExpedienteValidador validador, IServicioAutorizacion servicioAutorizacionProvisorio, ServicioActualizacionEstado servicio)
{
    public void EjecutarModificacionExpediente(Expediente expediente, Usuario usuario)
    {
        if (!servicioAutorizacionProvisorio.PoseeElPermiso(usuario.permisos, Permiso.ExpedienteModificacion))
        {
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para modificar expedientes.");
        }
        if (validador.Validar(expediente))
        {
            expediente = servicio.ActualizarEstado(expediente); //No importa lo que ponga el usuario si el tramite ultimo activa un cambio.
            expediente.ModificarUltimaFecha(usuario.id);
            repoExp.ModificarExpediente(expediente, usuario.id);
        }
        else 
            new ValidacionException($"El Expediente{expediente.Id} no fue validado"); 
    }
}