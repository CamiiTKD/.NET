namespace CGE.Aplicacion;

public class CasoDeUsoExpedienteAlta(IExpedienteRepositorio repo, ExpedienteValidador validador, IServicioAutorizacion servicio)
{
    public void EjecutarExpedienteAlta(Expediente expediente, int idUsuario)
    {
        if(!servicio.PoseeElPermiso(idUsuario, Permiso.ExpedienteAlta)){
            new AutorizacionException($"El usuario con id: {idUsuario} no posee permisos para dar de alta expedientes.");
        }
        if (validador.Validar(expediente))
        {
            repo.darDeAltaExpediente(expediente);
        }
    }
}