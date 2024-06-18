namespace CGE.Aplicacion;
public class CasoDeUsoTramiteModificacion(ITramiteRepositorio repoTra, IExpedienteRepositorio repoExp, ServicioActualizacionEstado servicio, IServicioAutorizacion servicioAutorizacion)
{
    public void EjecutarModificacionTramite(int idTramite, Usuario usuario) //otra opcion es recibir expediente id y preguntar por cual tramite quiero modificar.
    {
        if(!servicioAutorizacion.PoseeElPermiso(usuario.permisos, Permiso.TramiteModificacion)){
            new AutorizacionException($"El usuario con id: {usuario.id} no posee permisos para modificar trámites.");
        }
        bool salir = false;
        Tramite? tramite = repoTra.consultaPorTramiteId(idTramite);
        while (!salir)
        {
            ImprimirOpciones();
            string? opcionStr = Console.ReadLine();
            if(opcionStr != null){
                try
                {
                    int opcion = int.Parse(opcionStr);
                    switch (opcion)
                    {
                        case 0:
                            salir = true;
                            break;
                        case 1:
                            Console.Write("Ingrese el nuevo contenido:");
                            string? contenido = Console.ReadLine();
                            tramite.contenido = contenido;
                            break;
                        case 2:
                            try
                            {
                                tramite.tipo = ElegirEtiqueta(tramite.tipo);
                            }
                            catch
                            {
                                Console.WriteLine("Se esperaba un número válido");
                            }
                            break;

                        default:
                            Console.WriteLine("Se esperaba un número.");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Se esperaba un número.");
                }
            }
        }
        //podría usarse un bool para evitar que se modifique si no hay cambios
        Expediente? expediente = repoExp.consultaPorId(tramite.ExpedienteId);
        tramite.ModificarUltimaFecha(usuario.id);
        repoTra.ModificarTramite(tramite);
        servicio.ActualizarEstado(expediente);
        expediente = null;
        tramite = null;
    }

    // ------------------------------------- PRIVATE ---------------------------------------
    private EtiquetaTramite ElegirEtiqueta(EtiquetaTramite tipoActual)
    {
        Console.WriteLine("Seleccione la nueva etiqueta a asignar:");
        Console.WriteLine("[1] Escrito presentado.");
        Console.WriteLine("[2] Pase a estudio.");
        Console.WriteLine("[3] Despacho.");
        Console.WriteLine("[4] Resolución.");
        Console.WriteLine("[5] Notificación.");
        Console.WriteLine("[6] Pase al archivo.");
        Console.Write("Opcion: ");
        string? opcionStr = Console.ReadLine();
        if (opcionStr != null){
            int opcion = int.Parse(opcionStr);
            switch (opcion)
            {
                case 1:
                    return EtiquetaTramite.Escrito_Presentado;
                case 2:
                    return EtiquetaTramite.Pase_A_Estudio;
                case 3:
                    return EtiquetaTramite.Despacho;
                case 4:
                    return EtiquetaTramite.Resolucion;
                case 5:
                    return EtiquetaTramite.Notificacion;
                case 6:
                    return EtiquetaTramite.Pase_Al_Archivo;
                default:
                    Console.WriteLine("La opción seleccionada no existe, no se hicieron modificaciones.");
                    return tipoActual;
            }
        }
        else {
            return tipoActual;
        }
    }

    private void ImprimirOpciones()
    {
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("[1] Modificar el contenido del trámite.");
        Console.WriteLine("[2] Modificar etiqueta del trámite.");
        Console.WriteLine("[0] Salir.");
        Console.Write("Opcion: ");
    }
}