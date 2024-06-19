namespace CGE.Aplicacion;
public class CasoDeUsoExpedienteModificacion(IExpedienteRepositorio repoExp, ExpedienteValidador validador, IServicioAutorizacion servicioAutorizacionProvisorio, ServicioActualizacionEstado servicio)
{
    public void EjecutarModificacionExpediente(int idExpediente, Usuario usuario)
    {
        if (!servicioAutorizacionProvisorio.PoseeElPermiso(usuario.permisos, Permiso.ExpedienteModificacion))
        {
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para modificar expedientes.");
        }
        bool salir = false;
        Expediente? expediente = repoExp.consultaPorId(idExpediente);
        if (validador.Validar(expediente))
        {
            servicio.ActualizarEstado(expediente); //No importa lo que ponga el usuario si el tramite ultimo activa un cambio.
            expediente.ModificarUltimaFecha(usuario.id);
            repoExp.ModificarExpediente(expediente);
        }
        expediente = null;
    }
}