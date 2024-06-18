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
        while (!salir)
        {
            ImprimirOpciones();
            try
            {
                int opcion = int.Parse(System.Console.ReadLine() ?? "");
                switch (opcion)
                {
                    case 0:
                        salir = true;
                        break;
                    case 1:
                        System.Console.Write("Ingrese la nueva carátula: ");
                        string? caratula = System.Console.ReadLine();
                        expediente.caratula = caratula;
                        break;
                    case 2:
                        expediente.estado = ElegirEstado(expediente.estado);
                        break;
                    default:
                        System.Console.WriteLine("Opción de modificación inválida.");
                        break;
                }
            }
            catch
            {
                System.Console.WriteLine("Se esperaba un número.");
            }
        }
        if (validador.Validar(expediente))
        {
            servicio.ActualizarEstado(expediente); //No importa lo que ponga el usuario si el tramite ultimo activa un cambio.
            expediente.ModificarUltimaFecha(usuario.id);
            repoExp.ModificarExpediente(expediente);
        }
        expediente = null;
    }
    private void ImprimirOpciones()
    {
        System.Console.WriteLine("Seleccione una opcion:");
        System.Console.WriteLine("[1] Modificar la caratula.");
        System.Console.WriteLine("[2] Modificar el estado.");
        System.Console.WriteLine("[0] Salir.");
        System.Console.Write("Opcion: ");
    }

    private EstadoExpediente ElegirEstado(EstadoExpediente estadoActual)
    {
        Console.WriteLine("Seleccione el nuevo a asignar:");
        Console.WriteLine("[1] Recien Iniciado.");
        Console.WriteLine("[2] Para Resolver.");
        Console.WriteLine("[3] Con Resolucion.");
        Console.WriteLine("[4] En Notificacion.");
        Console.WriteLine("[5] Finalizado.");
        Console.Write("Opcion: ");
        try
        {
            int opcion = int.Parse(Console.ReadLine() ?? "");

            switch (opcion)
            {
                case 1:
                    return EstadoExpediente.Recien_Iniciado;
                case 2:
                    return EstadoExpediente.Para_Resolver;
                case 3:
                    return EstadoExpediente.Con_Resolucion;
                case 4:
                    return EstadoExpediente.En_Notificacion;
                case 5:
                    return EstadoExpediente.Finalizado;
                default:
                    System.Console.WriteLine("La opción seleccionada no existe, no se hicieron modificaciones.");
                    return estadoActual;
            }
        }
        catch
        {
            Console.WriteLine("Opción de estado inválida.");
        }
        return estadoActual;
    }
}