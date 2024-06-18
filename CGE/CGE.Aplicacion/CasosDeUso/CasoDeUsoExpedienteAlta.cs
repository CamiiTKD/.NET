namespace CGE.Aplicacion;

public class CasoDeUsoExpedienteAlta(IExpedienteRepositorio repo, ExpedienteValidador validador, IServicioAutorizacion servicio)
{
    public void EjecutarExpedienteAlta(Expediente expediente, Usuario usuario)
    {
        if(!servicio.PoseeElPermiso(usuario.permisos, Permiso.ExpedienteAlta)){
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para dar de alta expedientes.");
        }
        if (validador.Validar(expediente))
        {
            repo.darDeAltaExpediente(expediente);
        }
    }
}